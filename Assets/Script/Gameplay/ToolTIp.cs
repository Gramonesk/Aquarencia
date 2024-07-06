using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolTIp : MonoBehaviour
{
    public Transform player;
    public static ToolTIp Instance;
    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
    }
    public void Display()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
