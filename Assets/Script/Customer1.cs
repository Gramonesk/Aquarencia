//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//namespace rand
//{
//    public class Chance
//    {
//        /// <summary>
//        /// Try to get the percentage eg: 60% -> 60% chance to get it right
//        /// </summary>
//        /// <param name="Percentage"></param>
//        /// <returns></returns>
//        public static bool Try(int value)
//        {
//            return value > UnityEngine.Random.Range(0, 101);
//        }
//    }

//}
//public enum CustomerState
//{
//    Wandering, Idle, View, Leaving, Buying
//}
//public class Customer1 : MonoBehaviour
//{
//    [Serializable]
//    public class state_decide
//    {
//        public float success_percentage;
//        public CustomerPhase state;
//    }
//    private NavMeshAgent agent;

//    [Header("Default Settings")]
//    public float Budget;
//    [Range(0,100)]
//    public float Interest;

//    public List<state_decide> chances;
//    private Transform facing_direction;
//    //Interest affects behaviours, the higher it is the more likely the customer will buy something

//    [Header("Idle Settings")]
//    public float maxIdleTime;

//    [Header("Wandering Settings")]
//    public int maxWanderingCount;

//    [Header("View Settings")]
//    public float maxViewTime;
//    public GameObject item;
//    public LayerMask ViewedObjectLayer;

//    [Header("Leave Settings")]
//    public NPC_Queue _TicketCounter;

//    [Header("Buy Settings")]
//    public NPC_Queue _AlbumCounter;

//    StateMachine<CustomerState> SM = new();
//    void Awake()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        agent.updateUpAxis = false;
//        agent.updateRotation = false;
//        //Waypoints = Waypoints == null ? FindAllWaypoints() : Waypoints;
//        SM.AddState(CustomerState.Idle, new IdleState1(this));
//        SM.AddState(CustomerState.Wandering, new WanderState1(agent, this));
//        SM.AddState(CustomerState.View, new ViewingState1(agent, this));
//        SM.AddState(CustomerState.Leaving, new LeavingState1(agent, this));
//        SM.AddState(CustomerState.Buying, new BuyingState1(agent, this));
//        SM.setstate(CustomerState.Idle);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
