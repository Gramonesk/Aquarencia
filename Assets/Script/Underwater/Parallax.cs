using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Untuk sekarang belum ada patokan, \njadinya jalan sendiri")]
    public float speed;
    //public float parallaxMultiplier;

    private float startpos, length;
    private void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        transform.position = Vector3.right * Random.Range(-length / 4, length / 4);
        startpos = transform.position.x;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

        if (Mathf.Abs(transform.position.x) > Mathf.Abs(startpos - length))
        {
            transform.position = Vector3.right * startpos;
        }
    }
    //private float length, startpos;
    //public GameObject cam;
    //public float parallaxEffect;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    startpos = transform.position.x;
    //    length = GetComponent<SpriteRenderer>().bounds.size.x;
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    float distance = cam.transform.position.x * parallaxEffect;

    //    transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
    //}
}
