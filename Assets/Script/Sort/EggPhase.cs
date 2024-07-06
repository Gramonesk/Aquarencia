using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class EggPhase : BaseState, IDragStartPhase, IDragEndPhase, IDragPhase
{
    private float growth_time;
    private float time;
    Action<TurtleState> Switch;
    private List<Sprite> changes;
    private SpriteRenderer renderer;
    private TurtleEntity turtle;
    public EggPhase(Action<TurtleState> action, float minValue, float maxValue, SpriteRenderer turtle, List<Sprite> changes, TurtleEntity turtlebase)
    {
        Switch = action;
        growth_time = UnityEngine.Random.Range(minValue, maxValue);
        renderer = turtle;
        this.changes = changes;
        this.turtle = turtlebase;
    }
    public override void OnStart()
    {
        time = 0;
        renderer.sprite = changes[0];
    }
    public override void OnExit()
    {
        renderer.sprite = turtle.defaultSprite;
    }
    public override void OnUpdate()
    {
        time += Time.deltaTime;
        if (time > growth_time * 2 / 3) renderer.sprite = changes[2];
        else if (time > growth_time / 3) renderer.sprite = changes[1];
        if (time > growth_time)
        {
            Switch?.Invoke(TurtleState.Turtle);
        }
    }
    public void OnDrag() { }
    public void OnEndDrag() { }
    public void OnStartDrag() { }
}

//public class EggPhase : BaseState, IDragPhase, IDragEndPhase
//{
//    private float grow_time;
//    private float time;
//    Action<TurtleState> state;
//    public EggPhase(Action<TurtleState> state, float value)
//    {
//        this.state = state;
//        grow_time = value;
//    }

//    public override void OnStart()
//    {
//        Debug.Log("Start Egg");
//        time = 0;
//    }

//    public override void OnExit()
//    {

//    }

//    public override void OnUpdate()
//    {
//        time += Time.deltaTime;
//        if (time > grow_time)
//        {
//            state?.Invoke(TurtleState.Turtle);
//        }
//    }

//    public void OnDrag()
//    {

//    }

//    public void OnEndDrag()
//    {

//    }
//}
