using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class TurtlePhase : BaseState, IDragStartPhase, IDragEndPhase, IDragPhase
{
    Transform turtle; Rigidbody2D rb2d;
    float MinT, MaxT, MinD, MaxD;
    float time, distance, duration;

    bool isWalking;
    Vector2 direction;
    Vector2 difference = Vector2.zero;
    public TurtlePhase(Transform turtle, Rigidbody2D rb, float MinT, float MaxT, float MinD, float MaxD)
    {
        this.MinD = MinD;
        this.MaxD = MaxD;
        this.MinT = MinT;
        this.MaxT = MaxT;
        this.turtle = turtle;
        rb2d = rb;
    }
    public override void OnExit()
    {

    }
    public override void OnStart()
    {
        time = 0;
        duration = UnityEngine.Random.Range(MinT, MaxT);
        distance = UnityEngine.Random.Range(MinD, MaxD);
        direction = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2)).normalized * distance / duration;
        isWalking = UnityEngine.Random.Range(0, 101) < 60;
    }
    public override void OnUpdate()
    {
        time += Time.deltaTime;
        if (isWalking) rb2d.velocity = direction * Time.deltaTime * 20;
        if(time > duration)
        {
            rb2d.velocity = Vector2.zero;
            OnStart();
        }
    }
    public void OnStartDrag()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)turtle.position;
    }
    public void OnDrag() {
        rb2d.isKinematic = true;
        turtle.tag = "Untagged";
        turtle.position = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference);
    }
    public void OnEndDrag() 
    {
        rb2d.isKinematic = false;
        turtle.tag = "Turtle";
        OnStart();
    }

}

//public class TurtlePhase : BaseState, IDragPhase, IDragEndPhase
//{

//    protected RectTransform turtle;
//    protected Action<TurtleState> state;
//    protected float MinT, MaxT;
//    private float time, target;
//    public TurtlePhase(Action<TurtleState> state, float MinT, float MaxT, RectTransform turtle)
//    {
//        this.state = state;
//        this.MinT = MinT;
//        this.MaxT = MaxT;
//        this.turtle = turtle;
//    }
//    public override void OnExit()
//    {
        
//    }
//    public override void OnStart()
//    {
//        time = 0;
//        target = UnityEngine.Random.Range(MinT, MaxT);
//    }
//    public override void OnUpdate()
//    {
//        time += Time.deltaTime;
//        if(time > target)
//        {
//            float random = UnityEngine.Random.Range(0, 100);
//            if (random < 60) state?.Invoke(TurtleState.Walk);
//            else OnStart();
//        }
//    }
//    public void OnDrag()
//    {
//        //turtle.anchoredPosition += cursor.delta;
//    }
//    public void OnEndDrag()
//    {
//        float random = UnityEngine.Random.Range(0, 100);
//        if (random < 60) state?.Invoke(TurtleState.Walk);
//        else OnStart();
//    }
//}
