using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractEnemyBehaviour : MonoBehaviour
{
     [SerializeField] protected NavMeshAgent agent;
     [SerializeField] protected GameObject enemySoldier;
     [SerializeField] protected float normalSpeed;
     [SerializeField] protected float alarmSpeed;
     protected Animator animator;

    protected abstract void BehaviourControl();

    private void Awake() => agent = GetComponent<NavMeshAgent>();

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

    protected void ChangeSpeed()
    {
        if (agent.isStopped)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }
        BehaviourControl();
    }
}
