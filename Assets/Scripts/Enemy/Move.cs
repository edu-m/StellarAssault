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
    public static bool playerShoots;
     public Transform player;
    private bool seeAndSeekPlayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }
    // Start is called before the first frame update

    public void Start()
    {
        playerShoots = false;
        seeAndSeekPlayer = false;
    }

    public void Update()
    {
        ChangeEnemyPath();
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

    public void ChangeEnemyPath()
    {
        if (!DirectMode() && !seeAndSeekPlayer) //If an enemy has never seen the player and we're not in direct mode
        {//Normal path
            Debug.Log("Normal path");
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
        else if (!EnemyFieldOfView.canSeePlayer)
        {
            Debug.Log("Run to the player");
            agent.SetDestination(player.position); //If the enemy no longer sees the player, he will follow him
        }
        else
        {
            Debug.Log("Start shooting to the player");
            seeAndSeekPlayer = true;//The first time an enemy sees the player, he will no longer return to stealth mode
            EnemyShoot();//If the enemy can see the player
        }
    }
    
    
    public void EnemyShoot()
    {
        Debug.Log("Enter ShootRoutine");
        agent.SetDestination(player.position);

        if (!EnemyFieldOfView.canSeePlayer)
            return;
        else
            EnemyShoot();
    }


}
