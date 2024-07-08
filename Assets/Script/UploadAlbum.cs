using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UploadAlbum : MonoBehaviour, IDropHandler
{
    public UIAlbums album;

    public Image sprite;
    public UnityEvent OnDropImg;
    public Portal portal;
    public Draggable data;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.TryGetComponent(out Draggable drag))
        {
            UIAlbums newInventory = drag.drop.inventory;
            if (drag.Zone_parent == transform)
            {
                newInventory.Delete(drag);
                newInventory.Insert(drag, drag.drop, drag.Index + newInventory.offset * newInventory.SizePerPage);
            }
            else
            {
                sprite.sprite = drag.Image;
                data = drag;
                OnDropImg.Invoke();
            }
        }
    }
    public void OnConfirmation()
    {
        Player player = FindObjectOfType<Player>();
        var toClear = player.gameObject.GetComponentInChildren<IndexInventory>();
        foreach (var index in toClear.indexes) Inventory.Main.imgDatas[index].isSold = true;
        toClear.indexes.Clear();
        player.Day++;
        data.Details.isSold = false;
        album.Insert(data, 0);
        album.Refresh();
        portal.OnInteract();
    }

}
