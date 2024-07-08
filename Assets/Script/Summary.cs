using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Summary : MonoBehaviour
{
    public GameObject disp1;
    public GameObject disp2;
    public float time;
    public float money_gained;
    void Start()
    {
        Invoke(nameof(Wait), time);
    }
    void Wait()
    {
       disp1.SetActive(false); disp2.SetActive(true);
       disp2.GetComponentInChildren<TMP_Text>().text = money_gained.ToString();
       CurrencyManager.instance.AddValue(money_gained);
    }
}
