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
    static int numberOfDeadEnemies = 0;
    [SerializeField] public float health;

    public void Spawn(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        gameObject.SetActive(true);
    }
    public void Damage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy damaged, health is " + health);
        if (health <= 0)
            DeathEvent();
    }

    public float GetHealth() => health;

    protected IEnumerator DeathAnimationFade()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(5f);
        //ps.gameObject.SetActive(true);
        ps.Play();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        numberOfDeadEnemies++;
    }

    public void DeathEvent()
    {
        Debug.Log("Death Event");
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

    public static int GetNumberOfDeadEnemies()
    {
        return numberOfDeadEnemies;
    }

}
