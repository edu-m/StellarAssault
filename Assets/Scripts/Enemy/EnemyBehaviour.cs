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
    }

    // Update is called once per frame
    void Update()
    {   
        if(!Move.AlarmMode())
            animator.SetFloat("Speed", normalSpeed);
        else
            animator.SetFloat("Speed", alarmSpeed);
    }
}
