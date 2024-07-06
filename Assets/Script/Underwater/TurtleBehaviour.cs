using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnderwaterTurtle
{
    swimming,
    Pose
}
public class TurtleBehaviour : MonoBehaviour
{
    [Header("Turtle Settings")]
    public Transform _turtle;
    public Sprite spriteName;
    public float speed;
    public float timer;

    public StateMachine<UnderwaterTurtle> SM = new();
    private void Start()
    {
        spriteName = GetComponent<SpriteRenderer>().sprite;
        SM.AddState(UnderwaterTurtle.swimming, new Und_TurtleSwimPhase(_turtle, speed, timer));
        SM.AddState(UnderwaterTurtle.Pose, new Und_TurtlePosePhase());
        SM.setstate(UnderwaterTurtle.swimming);
        SM.OnEnter();
    }
    // Update is called once per frame
    void Update()
    {
        SM.OnUpdate();
    }
}
