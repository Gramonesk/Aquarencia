//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.InteropServices;
//using UnityEditor.PackageManager;
//using UnityEngine;
//using FileData;
//public class FileDataHandler
//{
//    private string dataDirPath = "";
//    private string dataFileName = "";
//    private string ImageFolderPath = "";
//    private bool useEncryption = false;
//    private readonly string EncryptWord = "Aquarencia";
//    private Texture2D texture;

//    public FileDataHandler(string dataDirPath, string dataFileName, string ImageFolderPath, bool useEncryption, Texture2D reference)
//    {
//        this.dataDirPath = dataDirPath;
//        this.dataFileName = dataFileName;
//        this.useEncryption = useEncryption;
//        this.ImageFolderPath = ImageFolderPath;
//        texture = reference;
//    }
//    public ImageData LoadData(GameData loaddeddata)
//    { 
//        ImageData imgdata = new ImageData();
//        foreach(string path in loaddeddata.Img_Paths)
//        {
//            string fullpath = Path.Combine(dataDirPath, ImageFolderPath, path);
//            byte[] png_byte = new byte[64];

//            if (File.Exists(fullpath))
//            {
//                try
//                {
//                    //ambil bytenya
//                    png_byte = File.ReadAllBytes(fullpath);

//                    ImageConversion.LoadImage(texture, png_byte);
//                    imgdata.Images.Add(texture);
//                }catch (Exception ex)
//                {
//                    Debug.LogError("Error occured when trying to load image from file " + fullpath + "\n" + ex);
//                }
                
//                #region comment
//                //int size = Marshal.SizeOf(typeof(tekstur));
//                //byte[] buffer = new byte[size];
//                //try
//                //{
//                //    using (FileStream fs = new FileStream(fullpath, FileMode.Open))
//                //    {
//                //        fs.Read(buffer, 0, size);
//                //    }
//                //    var gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
//                //    Texture2D result = (Texture2D)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(Texture2D));
//                //    Texture2D newScreenshot = new Texture2D(result.width, result.height, TextureFormat.RGB24, false);
//                //    newScreenshot.SetPixels(result.GetPixels());
//                //    newScreenshot.Apply();
//                //    tekstur a = new tekstur()
//                //    {
//                //        Image = newScreenshot
//                //    };
//                //    imgdata.Images.Add(a);
//                //    //imgdata.Images.Add(Sprite.Create(newScreenshot, new Rect(0, 0, newScreenshot.width, newScreenshot.height), new Vector2(0.5f, 0.5f)));
//                //}
//                //catch (Exception ex)
//                //{
//                //    Debug.LogError("Error occured when trying to load data from file " + fullpath + "\n" + ex);
//                //}
//                #endregion 
//            }
//        }
//        return imgdata;
//    }
//    public void SaveData(GameData loaddeddata, ImageData img)
//    {
//        string foldername = Path.Combine(dataDirPath, ImageFolderPath);

//        foreach (string path in loaddeddata.Img_Paths)
//        {
//            string fullpath = Path.Combine(dataDirPath, ImageFolderPath, path);
//            try
//            {
//                if(!File.Exists(foldername))Directory.CreateDirectory(foldername);
//                byte[] png_byte = ImageConversion.EncodeToPNG(img.Images[0]);
//                File.WriteAllBytes(fullpath + ".png", png_byte);
//                #region comment
//                //Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
//                //using (FileStream fs = new FileStream(fullpath, FileMode.Create))
//                //{
//                //    fs.Write(img.Images[0].Image.EncodeToJPG(), 0, Marshal.SizeOf(typeof(tekstur)));
//                //}
//                #endregion
//            }
//            catch (Exception ex)
//            {
//                Debug.LogError("Error occured when trying to save image from file " + fullpath + "\n" + ex);
//            }
//        }
//    }
//    public GameData Load()
//    {
//        string fullPath = Path.Combine(dataDirPath, dataFileName);
//        GameData loadeddata = null;
//        if (File.Exists(fullPath))
//        {
//            Debug.Log(fullPath);
//            try
//            {
//                string data2Load = "";
//                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
//                {
//                    using(StreamReader reader = new StreamReader(stream))
//                    {
//                        data2Load = reader.ReadToEnd();
//                    }
//                }
//                if (useEncryption)
//                {
//                    data2Load = EncryptDecrypt(data2Load);
//                }
//                loadeddata = JsonUtility.FromJson<GameData>(data2Load);
//            }catch (Exception e)
//            {
//                Debug.LogError("Error occured when trying to load data from file " + fullPath + "\n" + e);
//            }
//        }
//        return loadeddata;
//    }
//    public void Save(GameData data)
//    {
//        string fullPath = Path.Combine(dataDirPath, dataFileName);
//        try
//        {
//            if (!File.Exists(fullPath))Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

//            string data2Store = JsonUtility.ToJson(data, true);
//            if (useEncryption)
//            {
//                data2Store = EncryptDecrypt(data2Store);
//            }

//            using (FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate))
//            { 
//                using (StreamWriter writer = new StreamWriter(stream))
//                {
//                    writer.Write(data2Store);
//                }
//            }
//        }catch(Exception ex) 
//        {
//            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + ex);
//        }
//    }
//    private string EncryptDecrypt(string data)
//    {
//        string modifiedData = "";
//        for (int i = 0; i < data.Length; i++)
//        {
//            modifiedData += (char)(data[i] ^ EncryptWord [i % EncryptWord.Length]);
//        }
//        return modifiedData;
//    }
//}
