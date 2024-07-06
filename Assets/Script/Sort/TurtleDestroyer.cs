using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TurtleBehaviour>(out var hit))
        {
            Destroy(hit.gameObject);
        }
        //if (collision.tag != "Turtle") return;
        //Debug.Log("a");
        //TurtleBehaviour tutel = collision.GetComponent<TurtleBehaviour>();
        //if(tutel != null)
        //{
        //    tutel?.OnRelease(tutel);
        //}
    }
}
