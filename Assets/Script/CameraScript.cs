using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(this);
        GetBoundaries();
    }
    public void GetBoundaries()
    {
        GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = FindObjectOfType<PolygonCollider2D>();
    }
}
