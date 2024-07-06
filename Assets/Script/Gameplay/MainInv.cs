using System.Collections.Generic;
using UnityEngine;
using FileData;

public class MainInv : Inventory, ISaveFolder<ImageData>
{
    public FolderInfo Folder_info;
    public string File_name;
    public FolderInfo folder { get => Folder_info; set => Folder_info = value; }
    public string filename { get => File_name; set => File_name = value; }

    public void Awake()
    {
        Main = this;
    }
    public void Add(Sprite image, ImgDetails detail)
    {
        images.Add(image);
        imgDatas.Add(detail);
    }
    public void Remove(Sprite image, ImgDetails detail)
    {
        images.Remove(image); 
        imgDatas.Remove(detail);
    }
    public void Load(ImageData data)
    {
        List<Sprite> sprites = new();
        foreach (var image in data.Images)
        {
            sprites.Add(Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f)));
        }
        images = sprites;
        imgDatas = data.details;
    }

    public void Save(ref ImageData data)
    {
        List<Texture2D> textures = new();
        foreach (var image in images)
        {
            textures.Add(image.texture);
        }
        data.Images = textures;
        data.details = imgDatas;
    }
}
