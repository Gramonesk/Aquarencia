using UnityEngine;
using UnityEngine.Events;

public class UIClass : MonoBehaviour, ICommand
{
    public UnityEvent OnClick;
    public UnityEvent OnUndoClick;
    public void AddStack()
    {
        UIManagerInvoker.ExecuteCommand(this);
    }
    public void Execute()
    {
        OnClick?.Invoke();
    }
    public void Undo()
    {
        OnUndoClick?.Invoke();
    }
}
