using FileData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, ISaveFolder<PlayerData>
{
    public static CurrencyManager instance;
    private TMP_Text currency;
    [HideInInspector] public float money = 0;
    public string Filename;
    public FolderInfo Folder;
    public FolderInfo folder { get => Folder; set => Folder = value; }
    public string filename { get => Filename; set => Filename = value; }

    private void Awake()
    {
        instance = this;
        currency = GetComponent<TMP_Text>();
    }
    private void SetValue()
    {
        currency.text = money == 0 ? "0" : money.ToString("#.##");
    }
    public void AddValue(float value)
    {
        money += value;
        SetValue();
    }
    public void DeductValue(float value) 
    {
        money -= value;
        SetValue();
    }
    public void Load(PlayerData data)
    {
        money = data.currency;
        currency = GetComponent<TMP_Text>();
        SetValue();
    }

    public void Save(ref PlayerData data)
    {
        data.currency = money;
    }
}
