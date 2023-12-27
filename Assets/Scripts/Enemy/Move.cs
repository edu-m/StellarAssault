using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Move : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public Transform pointA;
    public Transform pointB;
    private bool MoveBack;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update

    private void Update()
    {
        if (MoveBack)
        {
            agent.SetDestination(pointB.position);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                 agent.SetDestination(pointA.position);
                 MoveBack = false;
            }
        }
        else
        {
            agent.SetDestination(pointA.position);
            if (agent.remainingDistance <= agent.stoppingDistance)
                MoveBack = true;
        }
    }
}
