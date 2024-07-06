using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class screenfield : MonoBehaviour
{
    private Camera cam;
    public BoxCollider2D camBox;
    private float sizeX, sizeY;
    void Start()
    {
        cam = GetComponent<Camera>();
        camBox = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        sizeY = cam.orthographicSize * 2;
        sizeX = sizeY * cam.aspect;
        camBox.size = new Vector2 (sizeX, sizeY);
    }
}
