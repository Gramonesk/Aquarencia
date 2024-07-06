using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;
    [Header("Main Settings")]
    public float TimeLimit;
    public bool DontpauseOnFinish;

    [Header("UI")]
    public GameObject TimeOut_Menu;

    bool isFinish;
    private float counter = 0;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        counter = 0;
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if(counter >= TimeLimit && !isFinish)
        {
            isFinish = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (!DontpauseOnFinish)setTimeScale(0);
            TimeOut_Menu.SetActive(true);
            AudioManager.Instance.Play("GameFinish");
        }
    }
    public void setTimeScale(int scale)
    {
        Time.timeScale = scale;
    }
}
