using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSort : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] public float MinigameTime;
    public static ScoreSort instance;

    [Space(2)]
    [Header("UI")]
    [SerializeField] private TMP_Text Round;
    [SerializeField] private TMP_Text Percentage;

    [SerializeField] public Summary summary;
    private float Score; private float round = 0;
    public float timeleft, time;
    private void Start()
    {
        instance = this;
        time = MinigameTime + Time.time;
    }
    private void Update()
    {
        timeleft = time - Time.time;
        if (timeleft <= 0) return;
        Round.text = "Time Left: " + (time - Time.time).ToString("#0.00");
    }
    public void OnMatch(bool condition)
    {
        if (timeleft <= 0) return;
        if (condition) Score++;
        round++;
        var a = (int)(Score / round * 100);
        Percentage.text = ((int)(Score / round * 100)).ToString();
        if (a > 75)
        {
            if (Score > 20) summary.money_gained = 150;
            else if (Score > 15) summary.money_gained = 100;
            else if (Score > 10) summary.money_gained = 50;
            Debug.Log("UPDATING, " + summary.money_gained);
        }
        else
        {
            summary.money_gained = 0;
        }
    }
}
