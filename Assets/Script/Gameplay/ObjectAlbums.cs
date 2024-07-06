using FileData;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IndexInventory))]
public class ObjectAlbums : Upgradeable
{
    [Header("Main Settings")]
    public UIAlbums target_Album;
    public int count = 4;
    private readonly List<GameObject> AlbumObjects = new();
    //ganti kalo semisal pake inv type lain
    private IndexInventory data;
    public UIClass Open;

    private void Awake()
    {
        data = GetComponent<IndexInventory>();
        for (int i = 0; i < count; i++)
        {
            AlbumObjects.Add(transform.GetChild(i).gameObject);
        }
    }
    private void Update()
    {
        Refresh();
    }
    public void Remove(GameObject target)
    {
        int index = data.imgDatas.FindIndex(i => i.slot == AlbumObjects.IndexOf(target));

        Inventory.Main.imgDatas[data.indexes[index]].isSold = true;
        target.GetComponentInParent<Interactable>().TurnOn();
        data.indexes.RemoveAt(index);
        data.Refresh();
    }
    public float GetPrice(GameObject target)
    {
        int index = data.imgDatas.FindIndex(i => i.slot == AlbumObjects.IndexOf(target));
        float price = data.imgDatas[index].price;
        AlbumObjects[index].SetActive(false);
        return price;
    }
    public void SetData()
    {
        target_Album.data = data;
        UIManagerInvoker.undoStack.Push(Open);
    }
    public void Refresh()
    {
        foreach(var obj in AlbumObjects)obj.SetActive(false);
        foreach(ImgDetails data in data.imgDatas)AlbumObjects[data.slot].SetActive(true);
    }
    public override void OnRefreshUpgrade()
    {
        GetComponent<Interactable>().TurnOn();
    }
}