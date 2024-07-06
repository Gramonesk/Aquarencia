using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TurtleFood : MonoBehaviour
{
    [Header("Main Settings")]
    public FoodSO FoodData;

    [Header("UI")]
    [SerializeField] private Image foodimage;
    [SerializeField] private TMP_Text text;

    public Action<Sprite> OnButtonDown;
    
    public void Start()
    {
        text.text = FoodData.button.ToString();
        foodimage.sprite = FoodData.food;
    }
    public void Update()
    {
        if (Input.GetKeyDown(FoodData.button))
        {
            OnButtonDown?.Invoke(FoodData.food);
        }
    }
}
