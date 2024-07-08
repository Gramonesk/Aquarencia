using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using FileData;
using System;
public class ScreenshotHandler : MonoBehaviour, ISaveFolder<ImageIndex>
{
    #region Variables
    [Header("ScreenShot Settings/Display")]
    public List<GameObject> canvasImages = new();
    public GameObject album;

    public FolderInfo folder_info;
    public string FileName;
    public FolderInfo folder { get => folder_info; set => _ = folder_info; }
    public string filename { get => FileName; set => _ = filename; }

    private int img_index;
    private float value;
    private screenfield field;
    private Collider2D[] collisions;
    public static ScreenshotHandler Instance;
    public static Image displayImage;
    //byte[] pngShot;
    Texture2D newScreenshot;
    Sprite SS_sprite;
    #endregion
    private void Awake()
    {
        Instance = this;
        field = GetComponent<screenfield>();
    }
    public void TakeSS()
    {
        StartCoroutine(TakeScreenshotAndShow());
        collisions = Physics2D.OverlapBoxAll(transform.position, field.camBox.size, 0);
    }
    public void SaveSS()
    {
        value = 20;
        foreach (Collider2D coll in collisions)
        {
            if(coll.TryGetComponent<IValuable>(out var val))value += val.AddScore();
        }
        if (value != 20) value = Mathf.Clamp(value, 50, 100);
        ImgDetails newImg = new()
        {
            path = null,
            price = value,
            slot = 0,
            isSold = false
        };
        img_index = DataManager.Instance.GetPathName(ref newImg);

        SaveToData(newImg);
        //Tambah jumlah jepretan
        Stats.instance.AddScore(Stats.instance.Texts.Count - 1);
        DataManager.Instance.SaveGame(null);
        if (GameCounter.Instance.Counter()) Resume();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UnderwaterCamera.Instance.GoBack();
    }
    public void SaveToData(ImgDetails detail)
    {
        var maindata = Inventory.Main;
        if (maindata.images.Count <= img_index)
        {
            maindata.images.Add(SS_sprite);
            maindata.imgDatas.Add(detail);
        }
        else
        {
            maindata.images[img_index] = SS_sprite;
            maindata.imgDatas[img_index] = detail;
        }
    }
    public IEnumerator TakeScreenshotAndShow()
    {
        foreach (var obj in canvasImages) obj.SetActive(false);
        yield return new WaitForEndOfFrame();
        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        foreach (var obj in canvasImages) obj.SetActive(true);
        Time.timeScale = 0f;
        newScreenshot = new Texture2D(screenshot.width,screenshot.height, TextureFormat.RGB24, false);
        newScreenshot.SetPixels(screenshot.GetPixels());
        newScreenshot.Apply();
        SS_sprite = Sprite.Create(newScreenshot, new Rect(0, 0, (int)displayImage.rectTransform.rect.width, (int)displayImage.rectTransform.rect.height), new Vector2(0.5f, 0.5f));

        album.SetActive(true);
        displayImage.enabled = true;
        displayImage.sprite = SS_sprite;
    }
    public void Load(ImageIndex data)
    {
    }
    public void Save(ref ImageIndex data)
    {
        data.index.Add(img_index);
    }
}
