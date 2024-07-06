using System.Collections.Generic;
using UnityEngine;
using FileData;
using UnityEngine.UI;
using System.Linq;

public class UISocialMedia : MonoBehaviour, ISaveFolder<PlayerData>
{
    public List<Image> images;

    private int day;
    public FolderInfo Folder;
    public string Filename;
    //ganti kalo butuh tipe lain
    public IndexInventory data;
    public FolderInfo folder { get => Folder; set => Folder = value; }
    public string filename { get => Filename; set => Filename = value; }

    private void Awake()
    {
        images = GetComponentsInChildren<Image>().ToList();
        images.RemoveAt(0);
    }
    public void Insert(IAlbumData alb)
    {
        data.Add(alb);
        day++;
        Refresh();
    }
    public void Refresh()
    {
        for(int i = 0; i < data.images.Count; i++)
        {
            if (i >= images.Count) return;
            images[i].sprite = data.images[i];
        }
    }
    public void Load(PlayerData data)
    {
        day = data.Day;
        Refresh();
    }

    public void Save(ref PlayerData data)
    {
        data.Day = day;
    }
}
