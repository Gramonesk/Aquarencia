using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FileData;

public abstract class Inventory : MonoBehaviour
{
    public static Inventory Main { get; set; }
    public List<Sprite> images = new();
    public List<ImgDetails> imgDatas = new();

    public List<int> indexes = new();
}
