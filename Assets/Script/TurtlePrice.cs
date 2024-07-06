using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IValuable
{
    public float AddScore();
}
[Serializable]
public struct Turtle_score
{
    public float score;
    public UnderwaterTurtle state;
}
public class TurtlePrice : MonoBehaviour, IValuable
{
    public List<Turtle_score> scores;
    Dictionary<UnderwaterTurtle, float> scoredict = new();
    private TurtleBehaviour turtle;
    public void Start()
    {
        turtle = GetComponent<TurtleBehaviour>();
        foreach(var score in scores)
        {
            scoredict.Add(score.state, score.score);
        }
    }
    public float AddScore()
    {
        Stats.instance.AddScore(turtle.spriteName);
        Camera cam = Camera.main;
        UnderwaterTurtle tstate = turtle.SM.curr_state;
        if (scoredict.ContainsKey(tstate))
        {
            float score = scoredict[tstate];
            //jika termasuk area tengah di kamera, scorenya begini
            var sizeY = cam.orthographicSize * 2 / 5;
            var sizeX = cam.aspect * sizeY;
            Vector2 difference = (cam.transform.position - transform.position).Abs();
            score *= Mathf.Clamp01(sizeX / difference.x);
            score *= Mathf.Clamp01(sizeY / difference.y);
            //Debug.Log(Mathf.Clamp01(sizeX / difference.x) + "X <-> Y" + Mathf.Clamp01(sizeY / difference.y));
            //jika cameranya ngezoom ke turtle, scorenya begini
            score *= Mathf.Clamp01(1.3f / cam.orthographicSize);
            return score;
        }
        else return 0;
    }
}
