using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SocialMedia : MonoBehaviour
{
    public UnityEvent OnToggle;
    public UnityEvent OnCancelToggle;
    bool a;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        if (!a) OnToggle?.Invoke();
        else OnCancelToggle?.Invoke();
        a = !a;
    }
}
