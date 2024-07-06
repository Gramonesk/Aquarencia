using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFeed : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private float MinigameTime;
    [SerializeField] private float maxInterpolationtime;
    [SerializeField] private float SlowestInterval, FastestInterval;
    [SerializeField] private FeedTurtle turtle;
    [Space(2)]
    [Header("UI")]
    [SerializeField] private TMP_Text Round;
    [SerializeField] private TMP_Text Percentage;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject Time_Over;

    [SerializeField] public Summary summary;
    private float Score; private float round = 0, interval = 0, elapsed = 0, time, timeleft;
    private void Start()
    {
        turtle.OnMatching = OnMatch;
        time = MinigameTime + Time.time;
    }
    private void Update()
    {
        timeleft = time - Time.time;
        if (timeleft <= 0) return;
        elapsed += Time.deltaTime;
        interval = Mathf.Lerp(SlowestInterval, FastestInterval, 1 - Mathf.Clamp01((time - Time.time)/maxInterpolationtime));
        Round.text = "Time Left: " + (time - Time.time).ToString("#0.00");
        slider.value = Mathf.Clamp01(elapsed / interval);
        if(interval < elapsed)
        {
            OnMatch(false);
        }
    }
    public void OnMatch(bool condition)
    {
        if (timeleft <= 0) return;
        elapsed = 0;
        turtle.GetNewFood();
        if (condition) Score++;
        round++;
        var a = (int)(Score / round * 100);
        Percentage.text = ((int)(Score / round * 100)).ToString();
        Debug.Log(a);
        if (a > 75)
        {
            if(Score > 40)summary.money_gained = 150;
            else if(Score > 30)summary.money_gained = 100;
            else if(Score > 20) summary.money_gained = 50;
        }
        else
        {
            summary.money_gained = 0;
        }
    }
}
