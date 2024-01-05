using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyData : MonoBehaviour, IDamageable
{
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
    public void Damage(int damage)
    {
        
        damage = (int)Mathf.Ceil(damage * (0.3f * (PlayerPrefs.GetInt("Difficulty") + 1)));
        Debug.Log("Damage is " + damage);
        health -= damage;
        if (health <= 0)
            DeathEvent();
    }

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

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        //ps.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

}
