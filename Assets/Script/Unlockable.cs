using FileData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradeable : MonoBehaviour, ISaveFolder<Upgrades>
{
    [Header("Upgrade-able Settings")]
    public upgrade_type type;
    [Tooltip("The order of this item to be unlocked, DEFAULT is 0")]
    public int Unlockable_SerialNumber;

    [Header("Save Settings")]
    public FolderInfo Folder;
    public string Filename = "upgrades";

    private int Unlocked_index;
    public FolderInfo folder { get => Folder; set => Folder = value; }
    public string filename { get => Filename; set => Filename = value; }

    public virtual void OnRefreshUpgrade()
    {
        gameObject.SetActive(true);
    }
    public void Load(Upgrades data)
    {
        Unlocked_index = UpgradeHandler.GetTypeValue(type, data);
        if (Unlockable_SerialNumber >= Unlocked_index + 1) return;
        OnRefreshUpgrade();
    }
    public void Save(ref Upgrades data)
    {
    }
}
