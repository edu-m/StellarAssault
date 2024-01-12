using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdLevelEnemyData : AbstractEnemyData
{
    private WaveSpawner waveSpawner;
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
    private void Start() => waveSpawner = GetComponentInParent<WaveSpawner>();
   protected override IEnumerator DeathAnimationFade()
   {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(5f);
        ps.Play();
        yield return new WaitForSeconds(0.1f);
        waveSpawner.waves[waveSpawner.GetCurrentWaveIndex()].enemiesLeft--;
        if(Random.Range(0f,1f) <= 0.1f)
        {
           powerUp = powerUps[Random.Range(0, powerUps.Count)];
           Instantiate(powerUp, transform.position, transform.rotation);
        }
        Destroy(gameObject);
   }
}
