using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : AbstractEnemyBehaviour
{
    protected override void BehaviourControl()
    {
        if (!Move.DirectMode() && !Move.seeAndSeekPlayer)
            animator.SetFloat("Speed", normalSpeed);
        else if (!EnemyFieldOfView.canSeePlayer)
            animator.SetFloat("Speed", alarmSpeed);//If the enemy no longer sees the player, he has to run in his direction
        else
            animator.SetFloat("Speed", normalSpeed);//If he can see him, he will walk and shoots
    }
}
