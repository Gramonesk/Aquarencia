using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurtleState
{
    egg, Turtle
}
[RequireComponent(typeof(Rigidbody2D))]
public class TurtleEntity : MonoBehaviour
{
    [Header("Egg Settings")]
    public float min_growth_time;
    public float max_growth_time;
    public List<Sprite> sprite_changes;

    [Header("Turtle Settings")]
    public float Min_Time;
    public float Max_Time;
    public float Min_Distance;
    public float Max_Distance;

    [Header("Sprite and Animation Settings")]
    public Sprite defaultSprite;

    public Action<TurtleEntity> OnRelease;
    private Transform turtle;
    private Rigidbody2D rb2d;
    private SpriteRenderer render;
    public StateMachine<TurtleState> SM = new StateMachine<TurtleState>();
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        turtle = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        SM.AddState(TurtleState.egg, new EggPhase(SM.MoveToState, min_growth_time, max_growth_time, render, sprite_changes, this));
        SM.AddState(TurtleState.Turtle, new TurtlePhase(turtle, rb2d, Min_Time, Max_Time, Min_Distance, Max_Distance));
        SM.setstate(TurtleState.egg);
        SM.OnEnter();
    }
    private void Update()
    {
        if (ScoreSort.instance.timeleft > 0)
        SM.OnUpdate();
    }
    private void OnMouseDown()
    {
        if (ScoreSort.instance.timeleft > 0)
        SM.OnStartDrag();
    }
    private void OnMouseDrag()
    {
        if (ScoreSort.instance.timeleft > 0)
        SM.OnDrag();
    }
    private void OnMouseUp()
    {
        if (ScoreSort.instance.timeleft > 0)
        SM.OnEndDrag();
    }
    public void OnScore()
    {
        OnRelease?.Invoke(this);
    }
}
