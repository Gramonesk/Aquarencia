using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnderwaterState
{
    Submarine,
    Camera,
    Screenshot
}
public class Und_SubPhase : BaseState
{
    List<GameObject> objs;
    private KeyCode toggle_key;
    Action<UnderwaterState> toggle;
    public Und_SubPhase(KeyCode code, Action<UnderwaterState> change, List<GameObject> objects)
    {
        toggle_key = code;
        toggle = change;
        objs = objects;
    }
    public override void OnExit()
    {
        foreach (var obj in objs) obj.SetActive(false);
    }

    public override void OnStart()
    {
        Camera.main.orthographicSize = 5;
        Camera.main.transform.position = Vector3.zero;
        foreach (var obj in objs) obj.SetActive(true);
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(toggle_key))
        {
            toggle(UnderwaterState.Camera);
        }
    }
}
public class Und_CamPhase : BaseState, ICommand
{
    private CinemachineConfiner2D confiner;
    private Action<UnderwaterState> toggle;
    private CinemachineVirtualCamera cam;
    private KeyCode toggle_key;
    private float lens_size, multiplier, cam_speed;
    private List<GameObject> objs;
    public Und_CamPhase() { }
    public Und_CamPhase(KeyCode code, CinemachineVirtualCamera cam, List<GameObject> Objects, float val, Action<UnderwaterState> toggle, float camsped)
    {
        toggle_key = code;
        this.cam = cam;
        this.lens_size = cam.m_Lens.OrthographicSize;
        this.objs = Objects;
        multiplier = val;
        this.toggle = toggle;
        this.cam_speed = camsped;
        confiner = Camera.main.GetComponent<CinemachineConfiner2D>();
    }
    public override void OnStart()
    {
        cam.m_Lens.OrthographicSize = lens_size;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        foreach (var obj in objs) obj.SetActive(true);  
        cam.enabled = true;
        UIManagerInvoker.undoStack.Push(this);
    }

    public override void OnUpdate()
    {
        Vector3 moveVector = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0);
        cam.m_Lens.OrthographicSize = Mathf.Clamp(cam.m_Lens.OrthographicSize - Input.mouseScrollDelta.y * multiplier, 0.5f, 4.9f);
        confiner.InvalidateCache();
        cam.transform.position += Vector3.ClampMagnitude(moveVector, 1) * Time.deltaTime * cam_speed;
        if (Input.GetKeyDown(toggle_key))
        {
            toggle(UnderwaterState.Submarine);
        }
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
        {
            toggle(UnderwaterState.Screenshot);
        }
    }
    public override void OnExit()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cam.enabled = false;
        foreach (var obj in objs) obj.SetActive(false);
    }
    public void Execute()
    {
    }

    public void Undo()
    {
        toggle(UnderwaterState.Submarine);
    }
}
public class Und_ScreenshotPhase : BaseState
{
    public Und_ScreenshotPhase() { }
    public override void OnExit()
    {
        UIManager2.instance.AllowSettings(true);
    }
    public override void OnStart()
    {
        UIManager2.instance.AllowSettings(false);
        ScreenshotHandler.Instance.TakeSS();
    }
    public override void OnUpdate()
    {

    }
}
public class UnderwaterCamera : Pausable
{
    public static UnderwaterCamera Instance;
    [Header("Main Settings")]
    public KeyCode ToggleKey;
    public Image SSannya;

    [Header("Submarine Phase Settings")]
    public List<GameObject> SubmarineObjects;

    [Header("Camera Phase Settings")]
    public List<GameObject> CameraObjects;
    public float Zoom_sens;
    public float Camera_speed;

    private StateMachine<UnderwaterState> SM = new StateMachine<UnderwaterState>();
    private CinemachineVirtualCamera _camera;
    public override void Awake()
    {
        base.Awake();
        Instance = this;
        _camera = GetComponent<CinemachineVirtualCamera>();
        ScreenshotHandler.displayImage = SSannya;

        SM.AddState(UnderwaterState.Camera, new Und_CamPhase(ToggleKey, _camera, CameraObjects, Zoom_sens, SM.MoveToState, Camera_speed));
        SM.AddState(UnderwaterState.Submarine, new Und_SubPhase(ToggleKey, SM.MoveToState, SubmarineObjects));
        SM.AddState(UnderwaterState.Screenshot, new Und_ScreenshotPhase());
        SM.setstate(UnderwaterState.Submarine);
        SM.OnEnter();
    }
    public void GoBack()
    {
        SM.MoveToState(UnderwaterState.Camera);
    }

    public override void RealUpdate()
    {
        SM.OnUpdate();
    }
}
