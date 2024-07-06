using JsonExtended;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace FileData
{
    public class DataHandler
    {
        #region Variable & Constructor related
        private string datapath = "";
        private string imagepath = "";
        Texture2D texture;
        public DataHandler(string datapath, string imagepath)
        {
            this.datapath = datapath;
            this.imagepath = imagepath;
        }
        #endregion
        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
        public T LoadImage<T>(string FolderPath, string FileName) where T : ImageData
        {
            T data = (T) new ImageData();
            Wrapper<ImgDetails> imgData = LoadText<Wrapper<ImgDetails>>(FolderPath, FileName);
            if (imgData.Items == null) return default;
            data.details = imgData.Items.ToList();

            foreach (ImgDetails detail in data.details)
            {
                string FilePath = Path.Combine(datapath, imagepath, detail.path) + ".png";
                if (File.Exists(FilePath))
                {
                    try
                    {
                        byte[] png_bytes;
                        png_bytes = File.ReadAllBytes(FilePath);
                        texture = new Texture2D(1920, 1080);
                        ImageConversion.LoadImage(texture, png_bytes);
                        data.Images.Add(texture);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("Error occured when trying to load image from file " + FilePath + "\n" + ex);
                    }
                }
            }
            return data;
        }
        public void SaveImage(ImageData data, string FolderPath, string FileName)
        {
            string foldername = Path.Combine(datapath, FolderPath);
            string ImagePath = Path.Combine(datapath, imagepath);
            int count = data.details.Count;

            if (!File.Exists(foldername)) Directory.CreateDirectory(foldername);
            if (!File.Exists(ImagePath)) Directory.CreateDirectory(ImagePath);
            SaveText(new Wrapper<ImgDetails>() { Items = data.details.ToArray() }, FolderPath, FileName);
            for (int i = 0; i < count; i++)
            {
                string fullpath = Path.Combine(datapath, imagepath, data.details[i].path);
                try
                {
                    byte[] png_byte = ImageConversion.EncodeToPNG(data.Images[i]);
                    File.WriteAllBytes(fullpath + ".png", png_byte);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error occured when trying to save image from file " + fullpath + "\n" + ex);
                }
            }
        }
        public T LoadText<T>(string FolderPath, string FileName) where T : new()
        {
            string textPath = Path.Combine(datapath, FolderPath, FileName);
            string data2load = "";
            if (!File.Exists(textPath))return new();
            try
            {
                using (StreamReader reader = new StreamReader(textPath))
                {
                    data2load = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured when trying to get all Path from file: " + textPath + "\n" + ex);
            }
            Debug.Log(textPath + "  : " + data2load);
            return JsonUtility.FromJson<T>(data2load);
        }
        public void SaveText<T>(T data, string FolderName, string FileName)
        {
            string fullPath = Path.Combine(datapath, FolderName, FileName);
            Debug.Log(fullPath);
            try
            {
                if (!File.Exists(fullPath)) Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string data2store = JsonUtility.ToJson(data);
                using (StreamWriter writer = new StreamWriter(fullPath, false))
                {
                    writer.Write(data2store);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + ex);
            }
        }
    }

}


