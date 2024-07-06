using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<Sprite> Reference;

    public static Stats instance;
    public List<TMP_Text> Texts;
    private List<int> Score = new();
    private void Awake()
    {
        Texts = GetComponentsInChildren<TMP_Text>().ToList();
        Score.AddRange(from text in Texts
                       select 0);
        instance = this;
        gameObject.SetActive(false);
    }
    public void AddScore(Sprite sprite)
    {
        int index = Reference.IndexOf(sprite);
        if(index != -1)
        {
            Score[index]++;
            Texts[index].text = Score[index].ToString();
        }
    }
    public void AddScore(int index)
    {
        if (index != -1)
        {
            Score[index]++;
            Texts[index].text = Score[index].ToString();
        }
    }
}
