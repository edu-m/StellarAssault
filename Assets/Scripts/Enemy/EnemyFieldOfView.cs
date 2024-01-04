using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    Transform target;
    Vector3 directionToTarget;
    float angleToTarget;

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

    bool IsWithinAngles()
    {
        // Calculate the direction vector from the reference point to the target
        // 2f is the height of the enemy, so vectors are given a y offset to make them eye-level
        directionToTarget = (target.position + Vector3.up * 2.5f - transform.position).normalized;
        // Calculate the angle between the reference direction and the direction to the target
        angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
        return angleToTarget >= -angle && angleToTarget <= angle;
    }

    private bool IsWithinRadius()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length == 0) 
            return false;
        target = rangeChecks[0].transform;
        directionToTarget = (target.position - transform.position).normalized;
        return true;
    }

    private void FOVCheck()
    {
        if (!IsWithinRadius() || !IsWithinAngles())
        {
            canSeePlayer = false;
            return;
        }
        float distanceToTarget=Vector3.Distance(transform.position, target.position);
        canSeePlayer = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
    }

// debug stuff
#if false

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void DebugRay()
    {

        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, angle / 2);

        Debug.DrawRay(transform.position + transform.up * 2f, directionToTarget * radius);

        Debug.DrawRay(transform.position + transform.up * 2f, transform.forward + viewAngle01 * radius + new Vector3(0, angle / 2, 0), Color.green);
        Debug.DrawRay(transform.position + transform.up * 2f, transform.forward + viewAngle02 * radius + new Vector3(0, angle / 2, 0), Color.green);

        Debug.DrawRay(transform.position + transform.up * 2f, transform.forward + viewAngle01 * radius + new Vector3(0, -angle / 2, 0), Color.green);
        Debug.DrawRay(transform.position + transform.up * 2f, transform.forward + viewAngle02 * radius + new Vector3(0, -angle / 2, 0), Color.green);
        //Debug.Log(directionToTarget);
    }

    private void Update()
    {
        //Debug.Log(angleToTarget);

        DebugRay();
    }
#endif
}
