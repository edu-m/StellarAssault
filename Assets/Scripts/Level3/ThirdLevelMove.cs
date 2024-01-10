using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.Build.Player;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ThirdLevelMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public Transform player;
    EnemyGun enemyGun;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyGun= GetComponent<EnemyGun>();
    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        EnemyFieldOfView.canSeePlayer = false;
    }

    public void Update() => EnemyPath();

    public void EnemyPath()
    {
        if (!EnemyFieldOfView.canSeePlayer)
        {
            //Debug.Log("Run to the player");
            agent.SetDestination(player.position); //If the enemy no longer sees the player, he will follow him
        
        }
        else
        {
            StartCoroutine(ShootRoutine());//If the enemy can see the player

        }
    }
    private IEnumerator ShootRoutine()
    {
        Shooting();
        if (!EnemyFieldOfView.canSeePlayer)
            yield break;
    }


    public void Shooting()
    {
        agent.stoppingDistance = 5f;
        agent.SetDestination(player.position);
        enemyGun.Shoot();
        agent.isStopped = agent.remainingDistance <= agent.stoppingDistance;
    }
}
