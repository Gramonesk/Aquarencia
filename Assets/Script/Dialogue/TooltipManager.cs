using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour {
    public float wait_seconds;
    public TMP_Text text;
    public Image display;
    public static TooltipManager _instance;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
    public void ShowToolTip(string message, Sprite image)
    {
        text.text = message;
        display.sprite = image;
        gameObject.SetActive(true);
    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
