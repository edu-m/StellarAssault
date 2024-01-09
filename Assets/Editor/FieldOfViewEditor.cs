using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EnemyFieldOfView))] 

public class NewBehaviourScript : Editor
{
    private void OnSceneGUI()
    {
        float angleOffset = 20f;
        EnemyFieldOfView fov = (EnemyFieldOfView)target;
        Handles.color=Color.white;
#if true
        Handles.DrawWireArc(fov.transform.position + new Vector3(0,angleOffset,0), Vector3.up, Vector3.forward, 360, fov.radius);
        Handles.DrawWireArc(fov.transform.position + new Vector3(0, -angleOffset, 0), Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.blue;
        Handles.DrawLine(fov.transform.position,fov.transform.position
            + viewAngle01
            * fov.radius
            + new Vector3(0, angleOffset, 0)
            );
        Handles.DrawLine(fov.transform.position, fov.transform.position
            + viewAngle02
            * fov.radius
            + new Vector3(0, angleOffset, 0)
            );

        Handles.DrawLine(fov.transform.position, fov.transform.position
            + viewAngle01
            * fov.radius
            + new Vector3(0, -angleOffset, 0)
            );
        Handles.DrawLine(fov.transform.position, fov.transform.position
            + viewAngle02
            * fov.radius
            + new Vector3(0, -angleOffset, 0)
            );

        if (EnemyFieldOfView.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player.transform.position);
        }
#endif
    }

    private Vector3 DirectionFromAngle(float eulerY,float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
