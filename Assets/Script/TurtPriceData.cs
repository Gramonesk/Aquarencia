using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turtle Score Data")]
public class TurtPriceData : ScriptableObject
{
    [Header("Turtle Settings")]
    public List<Turtle_score> Turtlescores;
}
