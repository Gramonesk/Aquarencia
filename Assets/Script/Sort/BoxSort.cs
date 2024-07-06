using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxSort : MonoBehaviour
{
    [Header("Main Settings")]
    public Sprite turtle;

    [Header("UI Settings")]
    public Image gambarTurtle;
    //public Action<bool> OnScore;
    private void Awake()
    {
        gambarTurtle.sprite = turtle;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("tutel detected");
        if(collision != null && collision.tag == "Turtle")
        {
            TurtleEntity tutel = collision.GetComponent<TurtleEntity>();
            if (turtle == tutel.defaultSprite) ScoreSort.instance.OnMatch(true);
            ScoreSort.instance.OnMatch(false);
            tutel?.OnRelease(tutel);
        }
    }
}
