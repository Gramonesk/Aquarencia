using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScrollerMovement : MonoBehaviour, IPlayerMove
{
    [Header("Main Settings")]
    [SerializeField] private float movement_speed;
    [SerializeField] private Transform look_direction;

    public bool _canMove = true;
    public bool canMove { get => _canMove; set => _canMove = value; }

    private Rigidbody2D rb2d;
    private Animator animator;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }
    public void Move()
    {
        float MoveMagnitude = Input.GetAxisRaw("Horizontal") * movement_speed;
        rb2d.velocity = new Vector2(MoveMagnitude, rb2d.velocity.y);
        if (MoveMagnitude < 0) look_direction.rotation = new Quaternion(0, 180, 0, 0);
        else if(MoveMagnitude > 0) look_direction.rotation = new Quaternion(0, 0, 0, 0);
    }
}
