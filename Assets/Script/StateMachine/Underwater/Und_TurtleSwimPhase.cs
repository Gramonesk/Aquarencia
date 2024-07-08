using UnityEngine;

public class Und_TurtleSwimPhase : BaseState
{
    private Transform _turtle;
    private float speed, timer, time;
    private Vector3 moveVector;
    public Und_TurtleSwimPhase(Transform _turtle, float speed, float timer)
    {
        this._turtle = _turtle;
        this.speed = speed;
        this.timer = timer;
    }
    public override void OnExit()
    {

    }

    public override void OnStart()
    {
        _turtle.position = new Vector2(_turtle.position.x, Random.Range(-3, 3));
        Set();
    }
    public override void OnUpdate()
    {
        _turtle.position += speed * Time.deltaTime * moveVector;
        if (time < Time.time) Set();
    }
    public void Set()
    {
        //Vector3 temp = Vector3.zero;
        moveVector.x = -Random.Range(speed / 3, speed);
        moveVector.y = Random.Range(moveVector.x / 4, -moveVector.x / 4);
        //moveVector = Vector3.Lerp(moveVector, temp, Random.Range(0,1));
        time = Time.time + timer;
        if (Mathf.Abs(_turtle.position.x) < 12)
        {
            if(Random.Range(1,100) < 40)
            {
                Debug.Log("Berpose");
                //change(UnderwaterTurtle.Pose);
            }
        }
    }
}
