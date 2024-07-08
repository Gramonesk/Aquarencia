using FileData;
using System.Collections.Generic;
using UnityEngine;

public class GameCounter : MonoBehaviour, ISaveFolder<Upgrades>
{
    public static GameCounter Instance;
    [Header("Main Settings")]
    public List<int> CounterLimit;

    [Header("UI")]
    public GameObject CounterOut_Menu;

    private int counter;
    public FolderInfo Folder;
    public string Filename;

    public FolderInfo folder { get => Folder; set => Folder = value; }
    public string filename { get => Filename; set => Filename = value; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Returns true if the game has not finished yet
    /// </summary>
    /// <returns></returns>
    public bool Counter()
    {
        counter--;
        if(counter <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            UIManager.instance.AllowSettings(false);
            CounterOut_Menu.SetActive(true);
            AudioManager.Instance.Play("GameFinish");
            return false;
        }
        return true;
    }
    public void setTimeScale(int scale)
    {
        Time.timeScale = scale;
    }
    public void Load(Upgrades data)
    {
        counter = CounterLimit[UpgradeHandler.GetTypeValue(upgrade_type.Inventory, data)];
    }
    public void Save(ref Upgrades data)
    {
    }
}
