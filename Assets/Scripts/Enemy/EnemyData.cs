using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyData : MonoBehaviour, IDamageable
{
    private const float maxHealth = 100f;
    private Animator animator;
    private ParticleSystem ps;
    private NavMeshAgent agent;
    [SerializeField] public float health;

    public void Spawn(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        gameObject.SetActive(true);
    }
    public void Damage(float damage)
    {
        Debug.Log("Damage");
        health -= damage;
        if (health <= 0)
            DeathEvent();
    }

    public float GetHealth() => health;

    IEnumerator DeathAnimationFade()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(5f);
        //ps.gameObject.SetActive(true);
        ps.Play();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    public void DeathEvent()
    {
        agent.isStopped = true;
        StartCoroutine(DeathAnimationFade());
    }
    private void Awake()
    {
        health = maxHealth;
        ps = GetComponent<ParticleSystem>();
        //ps.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

}
