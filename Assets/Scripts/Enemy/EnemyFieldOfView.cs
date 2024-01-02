using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class EnemyFieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;
    public GameObject player;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public static bool canSeePlayer;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            FOVCheck();
            //Debug.Log("Can see player " + canSeePlayer);
        }
    }

    bool IsWithinAngles(Vector3 targetPosition, float verticalFov)
    {
        // Calculate the direction vector from the reference point to the target
        // 2f is the height of the enemy, so vectors are given a y offset to make them eye-level
        Vector3 directionToTarget = (targetPosition + Vector3.up * 2f - transform.position).normalized;
        // Calculate the angle between the reference direction and the direction to the target
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        // Draw rays using Debug.DrawRay if debug is enabled
#if true
        Debug.DrawRay(transform.position + Vector3.up * 2f, directionToTarget * radius);
        Debug.DrawRay(transform.position +  Vector3.up * 2f, Quaternion.Euler(verticalFov, angle, 0) * transform.forward * radius, Color.green);
        Debug.DrawRay(transform.position +  Vector3.up * 2f, Quaternion.Euler(verticalFov, -angle, 0) * transform.forward * radius, Color.green);

        Debug.DrawRay(transform.position +  Vector3.up * 2f, Quaternion.Euler(-verticalFov, angle, 0) * transform.forward * radius, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * 2f, Quaternion.Euler(-verticalFov, -angle, 0) * transform.forward * radius, Color.green);
        Debug.Log(angleToTarget >= -verticalFov && angleToTarget <= verticalFov);
#endif
        return angleToTarget >= -verticalFov && angleToTarget <= verticalFov;
    }

    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (canSeePlayer)
        {
            canSeePlayer = false; 
            return; 
        }

        if (rangeChecks.Length == 0)
            return;

       Transform target = rangeChecks[0].transform;
       Vector3 directionToTarget = (target.position - transform.position).normalized;
       if(IsWithinAngles(target.position, angle))
       {
            float distanceToTarget=Vector3.Distance(transform.position, target.position);
            canSeePlayer = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
       }
       else
            canSeePlayer = false;
    }
}
