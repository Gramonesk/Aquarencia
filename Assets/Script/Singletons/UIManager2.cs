using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Pausable : MonoBehaviour
{
    private Action Do;
    public virtual void Awake()
    {
        Do = RealUpdate;
    }
    public void Update()
    {
        Do?.Invoke();
    }
    public abstract void RealUpdate();
    public void Pause() { Do = null; }
    public void Resume() { Do = RealUpdate; }
}
public class PauseController : Singleton<PauseController>
{
    List<Pausable> pausables;
    public override void Awake()
    {
        base.Awake();
        pausables = FindObjectsOfType<MonoBehaviour>(true).OfType<Pausable>().ToList();
    }
    public void Toggle(bool Pause)
    {
        Time.timeScale = Pause ? 0 : 1;
        if (Pause)foreach (var pausable in pausables) pausable.Pause();
        else foreach (var pausable in pausables) pausable.Resume();
    }
}
public class UIManager2 : MonoBehaviour, ICommand
{
    [Header("Animation Related Datas")]
    public SceneTransition pauseAnim;
    public static UIManager2 instance;

    private bool IsOn;
    [HideInInspector] public bool IsNotActive;
    Coroutine korotin;
    public void Awake()
    {
        instance = this;
        UIManagerInvoker.undoStack.Clear();
        UIManagerInvoker.undoStack.Push(this);
    }
    public void Update()
    {
        if (!IsNotActive && Input.GetKeyDown(KeyCode.Escape))
        {
            UndoAction();
        }
    }
    public void AllowSettings(bool con)
    {
        IsNotActive = !con;
    }
    public void UndoAction()
    {
        UIManagerInvoker.UndoCommand();
    }
    public void ChangeScene(string name)
    {
        SceneChanger.Instance.ChangeScene(name);
    }
    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void TogglePauseMenu()
    {
        if (korotin != null) StopCoroutine(korotin);
        korotin = StartCoroutine(ToggleMenu());
    }
    public IEnumerator ToggleMenu()
    {
        IsOn = !IsOn;
        if (IsOn)
        {
            PauseController.Instance.Toggle(true);
            yield return pauseAnim.AnimateTransitionIn();
        }
        else
        {
            PauseController.Instance.Toggle(false);
            yield return pauseAnim.AnimateTransitionOut();
        }
    }

    public void Execute()
    {
        TogglePauseMenu();
    }
    public void Undo()
    {
        TogglePauseMenu();
    }
}
