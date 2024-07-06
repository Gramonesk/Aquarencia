using FileData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Settings")]
    public float wait_seconds;
    public string message;
    public Image picture_display;
    private void Start()
    {
        wait_seconds = TooltipManager._instance.wait_seconds;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        StartCoroutine(StartShowToolTip());
    }
    IEnumerator StartShowToolTip()
    {
        yield return new WaitForSeconds(wait_seconds);
        message = this.GetComponent<IAlbumData>().Details.price.ToString("#.##") ?? "0";
        TooltipManager._instance.ShowToolTip(message, picture_display.sprite);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        TooltipManager._instance.HideToolTip();
    }
}
