using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FileData;

//g ush ud mainin Pausable
public interface IPlayerMove
{
    //public bool canMove { get; set; }
}
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : Pausable
{
    [Header("Main Settings")]
    [SerializeField] protected float movement_speed;
    [SerializeField] protected Transform look_direction;

    public bool _canMove = true;
    public bool canMove { get => _canMove; set => _canMove = value; }

    protected Rigidbody2D rb2d;
    protected Animator animator;

    public override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //DontDestroyOnLoad(this.gameObject);
    }
    public void Move()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * movement_speed;
        rb2d.velocity = direction;
        if (rb2d.velocity.magnitude > 0.2f)
        {
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);
            animator.SetFloat("Speed", rb2d.velocity.magnitude);
            Quaternion RotateDir = Quaternion.LookRotation(Vector3.forward, direction);
            look_direction.rotation = Quaternion.RotateTowards(transform.rotation, RotateDir,360);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
    public override void RealUpdate()
    {
        Move();
    }
}
