using System.Collections.Generic;
using UnityEngine;
using FileData;
using System.Linq;
public class UIAlbums : MonoBehaviour
{
    [Header("Resources")]
    public GameObject AlbumPrefab;

    [Header("Offset Settings")]
    public int Pages;
    public int SizePerPage;
    //ganti ini kalo semisal ga mau save index
    public IndexInventory data;

    [HideInInspector]public int offset;
    private List<DropZone> Zones = new();
    private void Awake()
    {
        Draggable.Canvas = GetComponentInParent<Canvas>().rootCanvas.transform;
        Zones = GetComponentsInChildren<DropZone>().ToList();
        foreach (var zone in Zones)
        {
            zone.inventory = this;
            int count = SizePerPage - zone.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                var obj = Instantiate(AlbumPrefab);
                obj.transform.SetParent(zone.transform);
            }
        }
    }
    public void Start()
    {
        offset = 0;
        Refresh();
    }
    public void Insert(IAlbumData album, int loc)
    {
        if (album.Image == null) return;
        album.Details.slot = loc;
        data.Add(album);
    }
    public void Insert(IAlbumData album, DropZone zone)
    {
        if (album.Image == null) return;
        album.Details.slot = Zones.IndexOf(zone);
        data.Add(album);
    }
    public void Insert(IAlbumData album, DropZone zone, int Index)
    {
        if (album.Image == null) return;
        album.Details.slot = Zones.IndexOf(zone);
        data.Add(album, Index);
    }
    public void Delete(IAlbumData album)
    {
        if (album.Image == null) return;
        data.Remove(album);
    }
    public void Refresh()
    {
        foreach (DropZone zone in Zones)
        {
            List<ImgDetails> album_in_zone;
            if (data.useMain) album_in_zone = data.imgDatas;
            else album_in_zone = data.imgDatas.FindAll(i => i.slot == Zones.IndexOf(zone));

            List<IAlbumData> albums = zone.GetComponentsInChildren<IAlbumData>(true).ToList();
            while (offset != 0 && album_in_zone.Count < offset * SizePerPage) offset--;
            int count = albums.Count;
            for (int i = 0; i < count; i++)
            {
                int j = i + offset * SizePerPage;
                if (j < album_in_zone.Count && !album_in_zone[j].isSold)
                {
                    albums[i].Image = data.images[data.imgDatas.IndexOf(album_in_zone[j])];
                    albums[i].Details = album_in_zone[j];
                    albums[i].Refresh();
                    albums[i].Object.SetActive(true);
                }
                else
                {
                    //albums[i].Image = null;
                    albums[i].Object.SetActive(false);
                }
            }
        }
    }
    public void Increment()
    {
        if (data.imgDatas.Count < SizePerPage || offset >= Pages)return;
        offset ++;
        Refresh();
    }
    public void Decrement()
    {
        if (offset <= 0) return;
        offset --;
        Refresh();
    }
}