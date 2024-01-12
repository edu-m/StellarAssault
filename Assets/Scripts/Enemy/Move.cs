using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.Build.Player;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Move : AbstractMove
{
    public Transform pointA;
    public Transform pointB;
    private bool MoveBack;
    public static bool playerShoots;
    public static bool seeAndSeekPlayer;

    protected override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyGun = GetComponent<EnemyGun>();
    }

    protected override void Start()
    {
        player = GameObject.Find("Player").transform;
        playerShoots = false;
        seeAndSeekPlayer = false;
        EnemyFieldOfView.canSeePlayer = false;
    }

    public override void RespondToSound() => playerShoots = true;

    public void Update() => EnemyPath();

    public static bool DirectMode()
    {
        if(!playerShoots && !EnemyFieldOfView.canSeePlayer)
            return false;
        //Debug.Log("Player shoots " + playerShoots + "Can see player " + EnemyFieldOfView.canSeePlayer);
        MusicManager.directMode = true;
        return true;
    }
    
    protected override void EnemyPath()
    {
        if (!DirectMode() && !seeAndSeekPlayer) //If an enemy has never seen the player and we're not in direct mode
        {//Normal path
            NormalPath();
        }
        else if (!EnemyFieldOfView.canSeePlayer)
        {
            //Debug.Log("Run to the player");
            agent.SetDestination(player.position); //If the enemy no longer sees the player, he will follow him
        }
        else
        {
            seeAndSeekPlayer = true;//The first time an enemy sees the player, he will no longer return to stealth mode
            StartCoroutine(ShootRoutine());//If the enemy can see the player
        }
    }

    public void NormalPath()
    {
        agent.stoppingDistance = 1f;
        //Debug.Log("Normal path");
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
