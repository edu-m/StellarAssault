using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.Build.Player;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ThirdLevelMove : AbstractMove
{
    protected override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyGun = GetComponent<EnemyGun>();
    }
    protected override void Start()
    {
        player = GameObject.Find("Player").transform;
        EnemyFieldOfView.canSeePlayer = false;
    }
   
    public void Update() => EnemyPath();

    protected override void EnemyPath()
    {
        //If the enemy no longer sees the player, he will follow him
        if (!EnemyFieldOfView.canSeePlayer)
            agent.SetDestination(player.position);
        //If the enemy can see the player
        else
            StartCoroutine(ShootRoutine());
    }
}
