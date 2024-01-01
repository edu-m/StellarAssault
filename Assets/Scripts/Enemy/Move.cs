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
    public static bool playerViewed = false;
     public Transform player;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update

    private void Update()
    {
        //StartCoroutine(MoveRoutine());
        if (!AlarmMode())
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
            agent.SetDestination(player.position);
        }
    }

    /*IEnumerator MoveRoutine()
    {
        Debug.Log(playerShoots);
        
        yield return new WaitUntil(() => playerShoots);
    }*/

    public static bool AlarmMode()
    {
        if(!playerShoots && !playerViewed)
        {
            return false;
        }
        return true;
    }
    public void RespondToSound(Sound shotSound)
    {
        Debug.Log("Enemy listens to sound and goes straight to it");
            playerShoots = true;
            MusicManager.directMode = true;
            
    }

    
}
