using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDest((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    public void SetDest(Vector2 position)
    {
        NavMeshPath CurrentPath = new();
        if (NavMesh.CalculatePath((Vector2)transform.position, position, NavMesh.AllAreas, CurrentPath))
        {
            Vector3[] corners = CurrentPath.corners;
            if(corners.Length > 2)
            {
                for (int i = 1; i < corners.Length; i++)
                {
                    corners[i] = corners[i].x > corners[i].y ? Vector3.right * corners[i].x
                        : Vector3.up * corners[i].y;

                }
                agent.SetDestination(position);
            }
            else
            {

            }

        }
    }
}
