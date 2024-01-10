using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private WaveSpawner waveSpawner;
    private const float maxHealth = 100f;
    private Animator animator;
    private ParticleSystem ps;
    private NavMeshAgent agent;
    [SerializeField] public float health;

    private void Awake()
    {
        health = maxHealth;
        ps = GetComponent<ParticleSystem>();
        //ps.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();
    }
    public void Damage(int damage)
    {
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
        waveSpawner.waves[waveSpawner.GetCurrentWaveIndex()].enemiesLeft--; 
        Destroy(gameObject);
    }

    public void DeathEvent()
    {
        agent.isStopped = true;
        StartCoroutine(DeathAnimationFade());
    }
   
}
