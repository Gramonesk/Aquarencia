using FileData;
using UnityEngine;
using UnityEngine.UI;

public class Posted_Object : MonoBehaviour, IAlbumData
{
    [Header("UI Settings")]
    public Image picture_display;
    public Sprite Image { get; set; }
    public GameObject Object { get => gameObject; }
    public ImgDetails Details { get; set; }
    public void Refresh()
    {
        picture_display.sprite = Image;
    }
}
