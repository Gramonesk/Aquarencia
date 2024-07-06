using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class FeedTurtle : MonoBehaviour
{
    [Header("Main Settings")]
    public List<TurtleFood> TurtleFoodList;
    public List<Sprite> TurtleSprites;

    [Header("UI")]
    public Image TurtleSprite;
    public Image FoodWanted;

    public Action<bool> OnMatching;
    public List<Sprite> Foods = new List<Sprite>();
    private Sprite WantedFood;
    void Start()
    {
        foreach(TurtleFood t in TurtleFoodList)
        {
            Foods.Add(t.FoodData.food);
            t.OnButtonDown = Match;
        }
        GetNewFood();
    }
    public void Match(Sprite food)
    {
        OnMatching?.Invoke(food == WantedFood);
    }
    public void GetNewFood()
    {
        TurtleSprite.sprite = TurtleSprites[UnityEngine.Random.Range(0, TurtleSprites.Count)];
        WantedFood = Foods[UnityEngine.Random.Range(0, TurtleFoodList.Count)];
        FoodWanted.sprite = WantedFood;
    }
}
