using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractEnemyData : MonoBehaviour, IDamageable
{
    protected const float maxHealth = 100f;
    protected Animator animator;
    protected ParticleSystem ps;
    protected NavMeshAgent agent;
    [SerializeField] protected float health;
    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
            DeathEvent();
    }
    public void DeathEvent()
    {
        agent.isStopped = true;
        StartCoroutine(DeathAnimationFade());
    }
    public float GetHealth() => health;

    protected abstract IEnumerator DeathAnimationFade();
}
