using UnityEngine;
using FileData;
using UnityEngine.Rendering.Universal;

public class IndexInventory : Inventory, ISaveFolder<ImageIndex>
{
    public FolderInfo Folder_info;
    public string File_name;
    public bool useMain;
    public FolderInfo folder { get => Folder_info; set => Folder_info = value; }
    public string filename { get => File_name; set => File_name = value; }
    public void Add(IAlbumData album)
    {
        int value = Main.imgDatas.IndexOf(album.Details);
        indexes.Add(value);
        Refresh();
    }
    public void Add(IAlbumData album, int index)
    {
        int value = Main.imgDatas.IndexOf(album.Details);
        index = Mathf.Clamp(index, 0, images.Count);
        indexes.Insert(index, value);
        Refresh();
    }
    public void Remove(IAlbumData album)
    {
        int value = Main.imgDatas.IndexOf(album.Details);
        indexes.Remove(value);
        Refresh();
    }
    public void Refresh()
    {
        var MainImgs = Main.images;
        var MainDetail = Main.imgDatas;
        images.Clear(); imgDatas.Clear();
        foreach (int idx in indexes)
        {
            images.Add(MainImgs[idx]);
            imgDatas.Add(MainDetail[idx]);
        }
        //images = MainImgs.FindAll(i => indexes.Contains(MainImgs.IndexOf(i)));
        //imgDatas = MainDetail.FindAll(i => indexes.Contains(MainDetail.IndexOf(i)));
    }
    public void Load(ImageIndex data)
    {
        if (useMain)
        {
            for (int i = 0; i < Main.images.Count; i++) indexes.Add(i);
        }
        else
        {
            indexes = data.index;
        }
        Refresh();
    }
    public void Save(ref ImageIndex data)
    {
        data.index = indexes;
    }
}