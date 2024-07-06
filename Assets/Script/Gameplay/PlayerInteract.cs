using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : Pausable
{
    [Header("Main Settings")]
    public KeyCode InteractButton;
    [Header("UI(Semi)")]
    public GameObject press_space;
    [HideInInspector] public Interactable interact;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out interact)) return;
        //if (interact.isDisabled) interact = null;
        //access via LayerSelection in Inspector
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Interactable temp)) return;
        interact = null;
        temp.UnHighlight();
    }

    public override void RealUpdate()
    {
        if (interact != null)
        {
            press_space.SetActive(true);
            interact.Highlight();
            if (Input.GetKeyDown(InteractButton))
            {
                interact.OnInteract?.Invoke();
            }
        }
        else
        {
            press_space.SetActive(false);
        }
    }
}
