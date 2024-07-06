using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}
public interface IPausable
{
    void OnPause();
    void OnResume();
}
public class UIManagerInvoker
{
    public static Stack<ICommand> undoStack = new();
    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
    }
    public static void UndoCommand()
    {
        if (undoStack.Count > 0)
        {
            if (undoStack.Count != 1)
            {
                ICommand activeCommand = undoStack.Pop();
                activeCommand.Undo();
            }
            else
            {
                undoStack.Peek().Execute();
            }
        }
    }
}
public class UIManager : MonoBehaviour, ICommand
{
    public SceneTransition pauseMenu;
    public List<GameObject> Unlisted_UIS;

    private List<IPausable> pauses;
    public static UIManager instance;
    private List<GameObject> Objects = new();
    private bool IsOn;
    [HideInInspector] public bool IsNotActive;
    Coroutine korotin;
    private void Awake()
    {
        pauses = FindObjectsOfType<MonoBehaviour>(true).OfType<IPausable>().ToList();
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
        for(int i = 0; i < transform.childCount; i++)
        {
            Objects.Add(transform.GetChild(i).gameObject);
        }
        foreach (var a in Unlisted_UIS) Objects.Remove(a);
        UIManagerInvoker.undoStack.Clear();
        UIManagerInvoker.undoStack.Push(this);
        //Objects = GetComponentsInChildren<GameObject>().ToList();
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
    public void Quit()
    {
        Application.Quit();
    }
    public void Toggle()
    {
        if(korotin != null)StopCoroutine(korotin);
        korotin = StartCoroutine(ToggleMenu());
    }
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public IEnumerator ToggleMenu()
    {
        IsOn = !IsOn;
        if (IsOn)
        {
            yield return pauseMenu.AnimateTransitionIn();
            Time.timeScale = 0;
            //PauseGame(true);
        }
        else
        {
            Time.timeScale = 1;
            //PauseGame(false);
            yield return pauseMenu.AnimateTransitionOut();
            foreach (GameObject obj in Objects) obj.SetActive(false);
        }
    }

    public void Execute()
    {
        Toggle();
    }
    public void Undo()
    {
        Toggle();
    }
    public void PauseGame(bool con)
    {
        if (con)
        {
            foreach (IPausable pause in pauses) pause.OnPause();
            Time.timeScale = 0;
            return;
        }
        else
        {
            foreach (IPausable pause in pauses) pause.OnResume();
            Time.timeScale = 1;
            return;
        }
    }
}
