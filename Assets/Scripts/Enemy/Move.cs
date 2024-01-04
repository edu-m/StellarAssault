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
    public static bool seeAndSeekPlayer;
    EnemyGun enemyGun;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
        enemyGun= GetComponent<EnemyGun>();

    }

    private void Start()
    {
        playerShoots = false;
        seeAndSeekPlayer = false;
        EnemyFieldOfView.canSeePlayer = false;
    }

    public void Update()
    {
        ChangeEnemyPath();
    }



    public static bool DirectMode()
    {
        if(!playerShoots && !EnemyFieldOfView.canSeePlayer)
            return false;
        //Debug.Log("Player shoots " + playerShoots + "Can see player " + EnemyFieldOfView.canSeePlayer);
        MusicManager.directMode = true;
        return true;
    }
    public void RespondToSound(Sound shotSound) => playerShoots = true;
    public void ChangeEnemyPath()
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
    
    
    private IEnumerator ShootRoutine()
    {
        //Debug.Log("Enter Shootroutine");
        Shooting();
        if (!EnemyFieldOfView.canSeePlayer)
        {
            //Debug.Log("Exit Shootroutine");
            //animator.SetBool("Shoot", false);
            yield break;
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

    public void Shooting()
    {
        //Debug.Log("Enter Shooting");
        agent.stoppingDistance = 5f;
        //animator.SetBool("Shoot", true);
        agent.SetDestination(player.position);
        enemyGun.Shoot();
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.isStopped = true;
        else
            agent.isStopped = false;
    }


}
