using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum CustomerPhase
{
    Idle,
    Wandering,
    Viewing,
    Buying,
    leaving,
    ticketcounter
}
public class WanderState : BaseState
{
    NavMeshAgent agent;
    Transform agent_transform;
    float maxRange;
    float wander_cap;
    Action Decide;
    public WanderState(NavMeshAgent agent, Customer cust)
    {
        this.agent = agent;
        agent_transform = agent.transform;
        maxRange = cust.maxRange;
        wander_cap = cust.maxWandering;
        Decide = cust.Decide;
    }
    public override void OnExit(){}
    public override void OnStart()
    {
        if(--wander_cap < 0)
        {
            Decide?.Invoke();
        }
        else
        {
            NavMeshHit Hit;
            bool isFound = NavMesh.SamplePosition((Vector2)agent_transform.position + UnityEngine.Random.insideUnitCircle * maxRange, out Hit, maxRange, NavMesh.AllAreas);
            if (!isFound) Debug.Log(Hit + "Not Found");
            agent.SetDestination(Hit.position);
        }
    }
    public override void OnUpdate()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("changing");
            agent.ResetPath();
            Decide?.Invoke();
        }
    }
}
public class LeavingState : BaseState
{
    NavMeshAgent agent;
    Vector3 doorPosition;
    float time, price;
    public LeavingState(NavMeshAgent agent, Customer cust){
        this.agent = agent;
        doorPosition = (Vector2)cust.DoorPosition.transform.position + cust.DoorPosition.offset;
        price = cust.MoneyGained;
    }
    public override void OnExit()
    {
    }
    public override void OnStart()
    {
        time = Time.time + 0.3f;
        Debug.Log(doorPosition);
        agent.SetDestination(doorPosition);
    }
    public override void OnUpdate()
    {
        //var sprite = agent.gameObject.GetComponent<SpriteRenderer>();
        //sprite.color = new Color(255, 255, 255, Mathf.Clamp01(agent.remainingDistance / agent.stoppingDistance * 2));
        time -= Time.time;
        if (agent.remainingDistance <= agent.stoppingDistance && time < 0f)
        {
            agent.ResetPath();
            CurrencyManager.instance.AddValue(price);
        }
    }
}
public class ViewingState : BaseState
{
    public BoxCollider2D box;
    public LayerMask layers;
    public NavMeshAgent agent;
    private Collider2D viewedObject;
    float minT, maxT, time, BuyChance;
    Action decide;
    Action<CustomerPhase> moveto;
    CustomerPhase interestedPhase;
    Customer cust;
    public ViewingState(NavMeshAgent agent, Customer cust)
    {
        this.box = cust.facing_direction.GetComponent<BoxCollider2D>();
        this.layers = cust.layers;
        this.agent = agent;
        this.minT = cust.minViewTime;
        this.maxT = cust.maxViewTime;
        this.BuyChance = cust.interestChance;
        this.interestedPhase = cust.InterestedState;
        decide = cust.Decide;
        moveto = cust.SM.MoveToState;
        this.cust = cust;
    }
    public override void OnExit()
    {
        if (viewedObject != null)
        {
            viewedObject.tag = "Untagged";
            if (cust.blockinteraction) viewedObject.GetComponentInParent<Interactable>().TurnOn();
            viewedObject = null;
        }
        agent.ResetPath();
    }
    public override void OnStart()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll((Vector2)box.transform.position, box.size, 0, layers);
        foreach (Collider2D coll in colls)
        {
            if(coll.tag != "Occupied")
            {
                viewedObject = coll;
                if(cust.blockinteraction)viewedObject.GetComponentInParent<Interactable>().TurnOff();
                NavMeshHit Hit;
                bool isFound = NavMesh.SamplePosition((Vector2)coll.transform.position + coll.offset, out Hit, 3, agent.areaMask);
                if (!isFound) Debug.Log(Hit + "Not Found");
                agent.SetDestination(Hit.position);
                time = UnityEngine.Random.Range(minT, maxT);
                break;
            }
        }
    }
    public override void OnUpdate()
    {
        Debug.Log("Viewing");
        if(viewedObject != null && agent.remainingDistance <= agent.stoppingDistance)
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                int chance = UnityEngine.Random.Range(0, 101);
                if (chance < BuyChance)
                {
                    cust.item = viewedObject.gameObject;
                    Debug.Log(cust.item);
                    moveto?.Invoke(interestedPhase);
                }
                else decide?.Invoke();
            }
        }else if(viewedObject == null)
        {
            decide?.Invoke();
        }
    }
}
public class IdleState : BaseState
{
    float max_idling_time;
    float min_idling_time;
    float waitTime;
    Action decide_next_phase;
    public IdleState(Customer cust)
    {
        decide_next_phase = cust.Decide;
        min_idling_time = cust.minIdleTime;
        max_idling_time = cust.maxIdleTime;
    }
    public override void OnExit()
    {
    }
    public override void OnStart()
    {
        waitTime = Time.time + UnityEngine.Random.Range(min_idling_time, max_idling_time);
    }
    public override void OnUpdate()
    {
        if(Time.time > waitTime)
        {
            decide_next_phase?.Invoke();
        }
    }
}
public class TicketState : BaseState
{
    NavMeshAgent agent;
    Action<CustomerPhase> moveto;
    NPC_Queue counter;
    //float waitTime, time;
    public TicketState(NavMeshAgent agent, Customer cust)
    {
        this.agent = agent;
        moveto = cust.SM.MoveToState;
        counter = cust.TicketCounter;
        //waitTime = cust.waitTime;
    }
    public override void OnExit()
    {
    }
    public override void OnStart()
    {
        Debug.Log("Going to ticket counter");
        counter.Enqueue(agent);
    }
    public override void OnUpdate()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.ResetPath();
            if (counter.Agents.IndexOf(agent) == 0)
            {
                counter.interactable.TurnOn();
                counter.interactable.OnInteract.AddListener(Do);
            }
        }
    }
    public void Do()
    {
        counter.interactable.OnInteract.RemoveListener(Do);
        moveto(CustomerPhase.leaving);
    }
}
public class BuyingState : BaseState
{
    NavMeshAgent agent;
    float price;
    Customer cust;
    NPC_Queue counter;
    ObjectAlbums obj;
    public BuyingState(NavMeshAgent agent, Customer cust)
    {
        this.agent = agent;
        this.cust = cust;
        this.counter = cust.AlbumCounter;
    }
    public override void OnExit()
    {
        obj.Remove(cust.item);
        CurrencyManager.instance.AddValue(price);
    }

    public override void OnStart()
    {
        obj = cust.item.GetComponentInParent<ObjectAlbums>();
        price = obj.GetPrice(cust.item);
        counter.Enqueue(agent);
    }

    public override void OnUpdate()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.ResetPath();
            if (counter.Agents.IndexOf(agent) == 0)
            {
                counter.interactable.TurnOn();
                counter.interactable.OnInteract.AddListener(Decide);
            }
        }
    }
    public void Decide()
    {
        counter.interactable.OnInteract.RemoveListener(Decide);
        cust.Decide();
    }
}
public class Customer : MonoBehaviour
{
    [Serializable]
    public class state_decide
    {
        public float success_percentage;
        public CustomerPhase state;
    }
    private NavMeshAgent agent;

    [Header("Chances")]
    [Tooltip("the higher in the hierarchy means it is more prioritized")]
    public List<state_decide> chances;
    public Transform facing_direction;
    public LayerMask layers;

    [Header("Idle State")]
    public float maxIdleTime;
    public float minIdleTime;

    [Header("Wandering State")]
    public float maxRange;
    public int maxWandering;

    [Header("Viewing State")]
    public float interestChance;
    public CustomerPhase InterestedState;
    public bool blockinteraction;
    public float minViewTime;
    public float maxViewTime;

    [Header("Buying State")]
    public NPC_Queue AlbumCounter;
    [HideInInspector] public GameObject item;

    [Header("CounterTicket State")]
    public NPC_Queue TicketCounter;
    public float waitTime;

    [Header("Leaving State")]
    public Collider2D DoorPosition;
    public float MoneyGained;

    //public static List<Transform> Waypoints;
    public StateMachine<CustomerPhase> SM = new();
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        //Waypoints = Waypoints == null ? FindAllWaypoints() : Waypoints;
        SM.AddState(CustomerPhase.Idle, new IdleState(this));
        SM.AddState(CustomerPhase.Wandering, new WanderState(agent, this));
        SM.AddState(CustomerPhase.Viewing, new ViewingState(agent, this));
        SM.AddState(CustomerPhase.leaving, new LeavingState(agent, this));
        SM.AddState(CustomerPhase.Buying, new BuyingState(agent, this));
        SM.AddState(CustomerPhase.ticketcounter, new TicketState(agent, this));
        SM.setstate(CustomerPhase.Idle);
    }
    private void Start()
    {
        SM.OnEnter();
    }
    private void Update()
    {
        SM.OnUpdate();
        if (agent.velocity.magnitude > 0.01f)
        {
            Quaternion RotateDir = Quaternion.LookRotation(Vector3.forward, agent.velocity.normalized);
            facing_direction.rotation = Quaternion.RotateTowards(transform.rotation, RotateDir, 360);
            animator.SetFloat("SpeedX", agent.velocity.x);
            animator.SetFloat("SpeedY", agent.velocity.y);
        }
        animator.SetFloat("Magnitude", agent.velocity.magnitude);
    }
    public void Decide()
    {
        foreach (state_decide decision in chances)
        {
            var chance = UnityEngine.Random.Range(0, 100f);
            if (decision.success_percentage >= chance)
            {
                SM.MoveToState(decision.state);
                break;
            }
        }
    }
        //public List<Transform> FindAllWaypoints()
        //{
        //    List<Transform> w_list = new();
        //    IEnumerable<Waypoint> transforms = FindObjectsOfType<Waypoint>();
        //    List<Waypoint> waypoints = new List<Waypoint>(transforms);
        //    foreach(Waypoint waypoint in waypoints)
        //    {
        //        w_list.Add(waypoint.transform);
        //    }
        //    return w_list;
        //}
}
