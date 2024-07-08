using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text textTurtle;
    //public Action<bool> OnScore;
    private void Awake()
    {
        gambarTurtle.sprite = turtle;
        textTurtle.text = gambarTurtle.sprite.name;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("Turtle"))
        {
            TurtleEntity tutel = collision.GetComponent<TurtleEntity>();
            if (turtle == tutel.defaultSprite) ScoreSort.instance.OnMatch(true);
            else ScoreSort.instance.OnMatch(false);
            tutel.OnScore();
        }
    }
}
