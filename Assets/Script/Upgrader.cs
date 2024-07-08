using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FileData;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class UpgradeHandler
{
    public static int GetTypeValue(upgrade_type type, Upgrades data)
    {
        return type switch
        {
            upgrade_type.Inventory => data.Inventory,
            upgrade_type.stabilizer => data.Stabilizer,
            upgrade_type.table => data.AlbumTable,
            _ => -1,
        };
    }
    public static void SetTypeValue(upgrade_type type, ref Upgrades data, int value)
    {
        switch(type)
        {
            case upgrade_type.Inventory :
                data.Inventory = value;
                return;
            case upgrade_type.table : 
                data.AlbumTable = value;
                return;
            case upgrade_type.stabilizer:
                data.Stabilizer = value;
                return;
            default:
                return;
        };
    }
}
public class Upgrader : MonoBehaviour, ISaveFolder<Upgrades>
{
    [Header("Main Settings")]
    public List<int> Cost = new();
    public upgrade_type type;

    [Header("UI Settings")]
    public TMP_Text price_text;
    public Sprite UnlockedSprite;
    public List<Image> images;

    [Header("Save Settings")]
    public FolderInfo Folder;
    public string Filename;

    public int unlocked = 0;
    public FolderInfo folder { get => Folder; set => Folder = value; }
    public string filename { get => Filename; set => Filename = value; }
    private void Update()
    {
        Debug.Log(name + " INI SLOTKU DAN INI NILAIKU " + unlocked);
    }
    public void Refresh()
    {
        for (int i = 0; i < unlocked; i++) images[i].sprite = UnlockedSprite;
        if (unlocked >= Cost.Count)
        {
            price_text.text = "";
            GetComponentInChildren<Button>().interactable = false;
            return;
        }
        price_text.text = Cost[unlocked].ToString();
    }
    public void TryBuy()
    {
        if(CurrencyManager.instance.money > Cost[unlocked])
        {
            CurrencyManager.instance.DeductValue(Cost[unlocked]);
            unlocked++;
            Debug.Log(unlocked);
            Refresh();
        }
    }
    public void Load(Upgrades data)
    {
        unlocked = UpgradeHandler.GetTypeValue(type, data);
        Refresh();
    }
    public void Save(ref Upgrades data)
    {
        UpgradeHandler.SetTypeValue(type,ref data,unlocked);
    }
}
