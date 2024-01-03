using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour 
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject enemySoldier;
    [SerializeField] float normalSpeed;
    [SerializeField] float alarmSpeed;
    Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        agent= GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        animator = enemySoldier.GetComponent<Animator>();
        StartCoroutine(SpeedRoutine());
    }

   
    private IEnumerator SpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            ChangeSpeed();
        }
    }

    private void ChangeSpeed()
    {
        if (!agent.isStopped)
        {
            if (!Move.DirectMode() && !Move.seeAndSeekPlayer)
                animator.SetFloat("Speed", normalSpeed);
            else if (!EnemyFieldOfView.canSeePlayer)
                animator.SetFloat("Speed", alarmSpeed);//If the enemy no longer sees the player, he has to run in his direction
            else
            {
                animator.SetFloat("Speed", normalSpeed);//If he can see him, he will walk and shoots
            }
                
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

}
