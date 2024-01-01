using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Move : MonoBehaviour, IHear
{
    [SerializeField] NavMeshAgent agent;
    public Transform pointA;
    public Transform pointB;
    private bool MoveBack;
    public static bool playerShoots = false;
     public Transform player;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update

    private void Update()
    {
        //StartCoroutine(MoveRoutine());
        if (!DirectMode())
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
        else
        {
            if (EnemyFieldOfView.canSeePlayer)
                StartCoroutine(ShootRoutine());
            
            if(playerShoots)
                agent.SetDestination(player.position); //If the enemy no longer sees the player, he will follow him
        }
    }

   

    public static bool DirectMode()
    {
        if(!playerShoots && !EnemyFieldOfView.canSeePlayer)
        {
            return false;
        }
        MusicManager.directMode = true;
        return true;
    }
    public void RespondToSound(Sound shotSound)
    {
        Debug.Log("Enemy listens to sound and goes straight to player");

            playerShoots = true;
    }

    IEnumerator ShootRoutine()
    {   

        if (!EnemyFieldOfView.canSeePlayer)
        {
            Debug.Log("ShootRoutine interrupted");
            yield break;
        }
    }


}
