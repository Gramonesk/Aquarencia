using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Queue : MonoBehaviour
{
    [Header("Queue Settings")]
    [Tooltip("Distance between queue")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private float offset;
    [SerializeField] private Vector2 direction;

    public List<NavMeshAgent> Agents = new();
    public Interactable interactable;
    private Vector2 position;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        position = startPosition.position;
    }
    public void Enqueue(NavMeshAgent agent)
    {
        Agents.Add(agent);
        Refresh();
    }
    public void Dequeue()
    {
        if (Agents.Count <= 0)return;
        Agents.RemoveAt(0);
        interactable.TurnOff();
        Refresh();
    }
    public void Refresh()
    {
        int count = Agents.Count;
        for (int i = 0; i < count; i++)
        {
            Agents[i].SetDestination(position + direction * offset * i);
        }
    }
}
