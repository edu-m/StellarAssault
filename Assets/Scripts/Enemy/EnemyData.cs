using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyData : AbstractEnemyData
{
    static int numberOfDeadEnemies = 0;

    public void Spawn(Transform spawnPoint)
    {
        transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        gameObject.SetActive(true);
    }

    protected override IEnumerator DeathAnimationFade()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(5f);
        //ps.gameObject.SetActive(true);
        ps.Play();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        numberOfDeadEnemies++;
    }

    private void Awake()
    {
        health = maxHealth;
        ps = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public static int GetNumberOfDeadEnemies() => numberOfDeadEnemies;

}
