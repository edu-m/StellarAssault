using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractMove : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected EnemyGun enemyGun;
    public Transform player;
    protected abstract void Awake();

    protected abstract void Start();
    protected void Shooting()
    {
        agent.stoppingDistance = 5f;
        agent.SetDestination(player.position);
        enemyGun.Shoot();
        agent.isStopped = agent.remainingDistance <= agent.stoppingDistance;
    }
    protected IEnumerator ShootRoutine()
    {
        Shooting();
        if (!EnemyFieldOfView.canSeePlayer)
            yield break;
    }
    public virtual void RespondToSound() {}

    protected abstract void EnemyPath();
}
