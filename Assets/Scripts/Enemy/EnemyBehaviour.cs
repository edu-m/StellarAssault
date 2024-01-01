using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour 
{
    [SerializeField] GameObject enemySoldier;
    [SerializeField] float normalSpeed;
    [SerializeField] float alarmSpeed;
    Animator animator;
    // Start is called before the first frame update
    
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
        if (!Move.DirectMode())
            animator.SetFloat("Speed", normalSpeed);
        else if (!EnemyFieldOfView.canSeePlayer)
                animator.SetFloat("Speed", alarmSpeed);//If the enemy no longer sees the player, he has to run in his direction
            else
                animator.SetFloat("Speed", normalSpeed);//If he can see him, he will walk and shoots
    }


}
