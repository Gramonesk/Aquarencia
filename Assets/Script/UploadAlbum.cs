using FileData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UploadAlbum : MonoBehaviour, IDropHandler
{
    public UIAlbums album;

    public Image sprite;
    public UnityEvent OnDropImg;
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
        data.Details.isSold = false;
        album.Insert(data, 0);
        album.Refresh();
        SceneChanger.Instance.ChangeScene("connector", OnLoad);
    }
    public void OnLoad()
    {
        
    }
}
