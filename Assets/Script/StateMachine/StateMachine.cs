using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class StateMachine<State> where State : Enum
{
    public Dictionary<State, BaseState> States = new Dictionary<State, BaseState>();
    public Dictionary<BaseState, List<Transition<State>>> conditions = new Dictionary<BaseState, List<Transition<State>>>();
    public BaseState current_state;
    public State curr_state;
    public void OnEnter()
    {
        current_state.OnStart();
    }
    public void OnUpdate()
    {
        current_state.OnUpdate();
        check();
    }
    public void OnExit()
    {
        current_state.OnExit();
    }
    public void OnDrag()
    {
        IDragPhase drag = (IDragPhase)current_state;
        if(drag != null)
        {
            drag.OnDrag();
        }
    }
    public void OnStartDrag()
    {
        IDragStartPhase drag = (IDragStartPhase)current_state;
        if (drag != null)
        {
            drag.OnStartDrag();
        }
    }
    public void OnEndDrag()
    {
        IDragEndPhase drag = (IDragEndPhase)current_state;
        if (drag != null)
        {
            drag.OnEndDrag();
        }
    }
    public void setstate(State state)
    {
        current_state = States[state];
        curr_state = state;
    }
    public void AddState(State state, BaseState baseState)
    {
        States.Add(state, baseState);
    }
    public void AddTransition(Transition<State> transition)
    {
        if (!conditions.ContainsKey(States[transition.origin]))
        {
            conditions.Add(States[transition.origin], new List<Transition<State>>());
        }
        conditions[States[transition.origin]].Add(transition);
    }
    public void MoveToState(State state)
    {
        current_state.OnExit();
        current_state = States[state];
        current_state.OnStart();
        curr_state = state;
    }
    public void check()
    {
        if (conditions.ContainsKey(current_state))
        {
            foreach (Transition<State> st in conditions[current_state])
            {
                if (st.checkcondition())
                {
                    current_state.OnExit();
                    current_state = States[st.next];
                    current_state.OnStart();
                    curr_state = st.next;
                    break;
                }
            }
        }
    }
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
    public class Transition<State> where State : Enum
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
    {
        Func<bool> condition;
        public State origin;
        public State next;
        public Transition(State from, State to, Func<bool> condition = null)
        {
            this.condition = condition;
            origin = from;
            next = to;
        }
        public bool checkcondition()
        {
            if (condition == null) return false;
            return condition();
        }
    }
}
