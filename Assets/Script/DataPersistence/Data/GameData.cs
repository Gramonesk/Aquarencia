using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace FileData
{
    //Simpen Data apa aja?? (IMAGE RELATED)
    // 1. List Image yg ada di Inventory
    // 2. List Image yg ada per Album Field (Pake Index bwt nandain dia yg mana)
    // ESTIMASI JUMLAH FOLDER :
    // - Album Inventory
    // - Save Data per object album (4 di Shop, 6 di Gallery)

    //Simpen Main Data apa??
    //1. Scene Terakhir??
    //2. NPC terakhir dimana??
    //3. Currency, Status

    //Simpen Player Data
    //Scene dia terakhir dimana
    //Position dia dimana
    //currency berapa
    public interface ISaveFolder<T>
    {
        public FolderInfo folder { get; set; }
        public string filename { get; set; }
        public void Load(T data);
        public void Save(ref T data);
    }
    public interface IAlbumData
    {
        public GameObject Object { get; }
        public Sprite Image { get; set; }
        public ImgDetails Details { get; set; }
        public void Refresh();
    }
    [Serializable]
    public class ImgDetails
    {
        public string path;
        public float price;
        public int slot;
        public bool isSold;

        public ImgDetails()
        {
            path = null;
            price = 0;
            slot = 0;
            isSold = true;
        }
    }
    [Serializable]
    public class ImageData
    {
        public List<Texture2D> Images;
        public List<ImgDetails> details;
        public ImageData()
        {
            Images = new();
            details = new();
        }
    }
    [Serializable]
    public class ImageIndex
    {
        public ImageIndex() {
            index = new();
        }
        public List<int> index;
    }
    
    public enum upgrade_type
    {
        Inventory, table, stabilizer
    }
    /// <summary>
    /// simpen index aj
    /// </summary>
    [Serializable]
    public class Upgrades
    {
        public Upgrades(){
            Inventory = 0;
            AlbumTable = 0;
            Stabilizer = 0;
        }
        public int Inventory;
        public int AlbumTable;
        public int Stabilizer;
    }
    [Serializable]
    public class PlayerData { 
        public PlayerData()
        {
            position = Vector2.zero;
            currency = 0;
            Day = 1;
            TurtleProgress = new();
        }
        public Vector3 position;
        public float currency;

        public int Day;
        public List<int> TurtleProgress;
    }

    //public interface ITableData
    //{
    //    public string folder_path { get; set; }
    //    public void Load(TableInfo data);
    //    public void Save(ref TableInfo data);
    //}
    //[Serializable]
    //public class TableInfo
    //{
    //    public List<int> Actives;
    //    public TableInfo()
    //    {
    //        Actives = new List<int>();
    //    }
    //}

    //[Serializable]
    //public class GameData
    //{
    //    //the default value of this;
    //    public GameData()
    //    {
    //        currency = 0;
    //        Img_Paths = new List<string>();
    //    }
    //    public int currency;
    //    public List<string> Img_Paths;
    //}

    //[Serializable]
    //public class ImageData
    //{
    //    public List<Texture2D> Images;
    //    public Dictionary<string, Texture2D> image;
    //    public ImageData()
    //    {
    //        Images = new List<Texture2D>();
    //    }

    //}
}
