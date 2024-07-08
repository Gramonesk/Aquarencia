using Unity.VisualScripting;
using UnityEngine.EventSystems;

public interface IDragPhase
{
    public void OnDrag();
}
public interface IDragEndPhase
{
    public void OnEndDrag();
}
public interface IDragStartPhase
{
    public void OnStartDrag();
}
public abstract class BaseState
{
    /// <summary>
    /// dont forget to add the main data to this
    /// </summary>
    public BaseState() { }
    public abstract void OnStart();
    public abstract void OnExit();
    public abstract void OnUpdate();
}

