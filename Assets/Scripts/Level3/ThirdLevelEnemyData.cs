using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdLevelEnemyData : MonoBehaviour, IDamageable
{
    private WaveSpawner waveSpawner;
    private const float maxHealth = 100f;
    private Animator animator;
    private ParticleSystem ps;
    private NavMeshAgent agent;
    [SerializeField] public float health;
    [SerializeField] public List<GameObject> powerUps;
    GameObject powerUp;

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

    public float GetHealth() => health;
    public void Damage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy damaged, health is " + health);
        if (health <= 0)
            DeathEvent();
    }
    public void DeathEvent()
    {
        Debug.Log("Death Event");
        agent.isStopped = true;
        //Add possible power-up spawn
        StartCoroutine(DeathAnimationFade());
    }
    protected  IEnumerator DeathAnimationFade()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(5f);
        //ps.gameObject.SetActive(true);
        ps.Play();
        yield return new WaitForSeconds(0.1f);
        waveSpawner.waves[waveSpawner.GetCurrentWaveIndex()].enemiesLeft--;
        if(Random.Range(0f,1f) <= 1f)
        {
            powerUp = powerUps[(int)Random.Range(0, 2)];
           Instantiate(powerUp, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

   
   
}
