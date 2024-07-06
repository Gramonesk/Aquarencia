using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace FileData
{
    public class DataManager : MonoBehaviour
    {
        #region Variables
        public static DataManager Instance;
        private DataHandler handler;
        public int MaxSize;

        //List Folder yg mau dipake
        private readonly Dictionary<(string,string), ImageData> ImageFolders = new();
        private readonly Dictionary<(string,string), PlayerData> PlayerFolders = new();
        private readonly Dictionary<(string, string), ImageIndex> IndexFolders = new();
        private readonly Dictionary<(string, string), Upgrades> UpgradeFolders = new();
        private readonly Dictionary<string, ImgDetails> Alldetails = new();
        //private readonly Dictionary<string, TableInfo> TableFolders = new();
        public PlayerData playerdata { get; private set; }

        //List Object yg punya fungsi save
        private List<ISaveFolder<ImageData>> DataImageObjects;
        private List<ISaveFolder<PlayerData>> _playerObject;
        private List<ISaveFolder<ImageIndex>> AllIndexObjects;
        private List<ISaveFolder<Upgrades>> upgradedataObjects;
        #endregion
        public void Awake()
        {
            Instance = this;
            handler = new(Application.persistentDataPath, "Images");
            DataImageObjects = FindInterfaceObjects<ISaveFolder<ImageData>>();
            _playerObject = FindInterfaceObjects<ISaveFolder<PlayerData>>();
            AllIndexObjects = FindInterfaceObjects<ISaveFolder<ImageIndex>>();
            upgradedataObjects = FindInterfaceObjects<ISaveFolder<Upgrades>>();
            LoadGame();
        }
        public void NewGame()
        {
            Debug.Log("new");
        }
        public void LoadGame()
        {
            //Access all object, reference the folder, What function to use to add the folder to the ref folder
            Load(DataImageObjects, ImageFolders, AddImage<ImageData, ISaveFolder<ImageData>>);
            Load(_playerObject, PlayerFolders, AddFolder<PlayerData, ISaveFolder<PlayerData>>);
            Load(AllIndexObjects, IndexFolders, AddFolder<ImageIndex, ISaveFolder<ImageIndex>>);
            Load(upgradedataObjects, UpgradeFolders, AddFolder<Upgrades, ISaveFolder<Upgrades>>);
        }
        public void SaveGame()
        {
            //Access all object, reference the folder, What function to use to save the items
            GetDatas(DataImageObjects, ImageFolders, handler.SaveImage);
            GetDatas(_playerObject, PlayerFolders, handler.SaveText);
            GetDatas(AllIndexObjects, IndexFolders, handler.SaveText);
            GetDatas(upgradedataObjects, UpgradeFolders, handler.SaveText);
        }
        /// <summary>
        /// Get a non-existing data name
        /// </summary>
        /// <param name="details_reference"></param>
        /// <returns></returns>
        public int GetPathName(ref ImgDetails details)
        {
            for(int index = 0; index < MaxSize; index++)
            {
                string name = "Album" + index.ToString();
                if (Alldetails.TryGetValue(name, out ImgDetails detail))
                {
                    if (detail.isSold)
                    {
                        details.path = name;
                        Alldetails[name] = details;
                        return index;
                    }
                }
                else
                {
                    details.path = name;
                    Alldetails.Add(name, details);
                    return index;
                }
            }
            Debug.Log("max cap has achieved");
            return -1;
        }
        private void OnApplicationQuit()
        {
            SaveGame();
        }
        public List<T> FindInterfaceObjects<T>()
        {
            IEnumerable<T> objects = FindObjectsOfType<MonoBehaviour>(true).OfType<T>();
            return new List<T>(objects);
        }
        public void Load<T, T1>(List<T1> Objects, Dictionary<(string, string), T> Folders, Func<(string, string) ,Dictionary<(string, string), T>, T> Add ) where T1 : ISaveFolder<T> where T : new()
        {
            T curr_data = new();
            foreach (T1 data in Objects)
            {
                if (data.folder == null) { Debug.LogWarning(data.ToString() + "Has not been applied yet"); return; }
                string FolderName = data.folder.Folder_Name;
                string FileName = data.filename;
                if (!Folders.ContainsKey((FolderName, FileName))) curr_data = Add((FolderName, FileName), Folders);
                if (curr_data != null) data.Load(curr_data);
            }
        }
        public T AddFolder<T,T1>((string, string) FolderName, Dictionary<(string, string), T> Folders) where T : new()
        {
            Folders.Add(FolderName, handler.LoadText<T>(FolderName.Item1, FolderName.Item2));
            return Folders[FolderName];
        }
        public T AddImage<T, T1>((string, string) FolderName, Dictionary<(string, string), T> Folders) where T : ImageData
        {
            Folders.Add(FolderName, handler.LoadImage<T>(FolderName.Item1, FolderName.Item2));
            if (Folders[FolderName] != null)
            {
                foreach (var folder in Folders[FolderName].details)
                {
                    if (folder != null)
                    {
                        Alldetails.TryAdd(folder.path, folder);
                    }
                }
            }
            return Folders[FolderName];
        }
        public void GetDatas<T, T1>(List<T1> Objects, Dictionary<(string, string), T> Folders, Action<T, string, string> SaveFunction) where T1 : ISaveFolder<T> where T : new()
        {
            foreach (T1 data in Objects)
            {
                if (data.folder == null) { Debug.LogWarning(data.ToString() + "Has not been applied yet"); return; }
                string FolderName = data.folder.Folder_Name;
                string FileName = data.filename;
                T curr_file = new();

                if (!Folders.ContainsKey((FolderName, FileName))) Debug.LogError((FolderName, FileName) + " Not found");
                curr_file = Folders[(FolderName, FileName)] ?? curr_file;

                data.Save(ref curr_file);
                Folders[(FolderName, FileName)] = curr_file;
            }
            foreach (((string, string) FolderName, T data) in Folders)
            {
                SaveFunction?.Invoke(data, FolderName.Item1, FolderName.Item2);
            }
        }
    }

}


