using FileData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IAlbumData
{
    [Header("UI Settings")]
    public Image picture_display;
    public static Transform Canvas;
    public static GameObject placeholder;

    [HideInInspector] public DropZone drop;
    [HideInInspector] public Transform Zone_parent;
    [HideInInspector] public int Index, prevIndex;
    public Sprite Image { get; set; }
    public GameObject Object { get => gameObject;}
    public ImgDetails Details { get; set; }
    public void Refresh()
    {
        picture_display.sprite = Image;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Zone_parent = transform.parent;
        Index = prevIndex = transform.GetSiblingIndex();
        drop = Zone_parent.GetComponent<DropZone>();

        if(placeholder == null)
        {
            placeholder = new();
            var placeholder_rect = placeholder.AddComponent<RectTransform>();
            LayoutElement le = placeholder.AddComponent<LayoutElement>();
            Rect alb_size = GetComponent<RectTransform>().rect;

            placeholder_rect.sizeDelta = new Vector2(alb_size.width, alb_size.height);
            le.flexibleHeight = 0;
            le.flexibleWidth = 0;
        }
        placeholder.transform.SetParent(Zone_parent);
        placeholder.transform.SetSiblingIndex(Index);
        placeholder.SetActive(true);

        transform.SetParent(Canvas);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //this.transform.position += (Vector3)eventData.delta;
        transform.position = eventData.position;

        int count = drop.ChildCount + 1; //+1 karena yg ini technically udh keluar
        if(Mathf.Abs(transform.position.y - placeholder.transform.position.y) <= 300)
        {
            if (Index + 1 < count && Zone_parent.GetChild(Index + 1).position.x < transform.position.x)Index++;
            if (Index - 1 >= 0 && Zone_parent.GetChild(Index - 1).position.x > transform.position.x)Index--;
            placeholder.transform.SetSiblingIndex(Index);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(Zone_parent);
        transform.SetSiblingIndex(Index);

        placeholder.SetActive(false);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        drop.inventory.Refresh();
    }
}
