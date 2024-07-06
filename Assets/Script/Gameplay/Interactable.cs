using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Settings")]
    public Color Highlight_color;

    private SpriteRenderer sr;
    private readonly List<SpriteRenderer> childs = new();
    public UnityEvent OnInteract;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        var trans = transform;
        int x = trans.childCount;
        for (int i = 0; i < x; i++) childs.Add(trans.GetChild(i).GetComponent<SpriteRenderer>());
    }
    public void TurnOn()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactables");
    }
    public void TurnOff()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInteract>().interact = null;
        UnHighlight();
    }
    public void Highlight()
    {
        sr.color = Highlight_color;
        foreach(var child in childs) child.color = Highlight_color;
    }
    public void UnHighlight()
    {
        sr.color = Color.white;
        foreach(var child in childs) child.color = Color.white;
    }
}
