using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    public GameObject _object;
    private void Update()
    {
        // This is a debug event. Remove in final release!
        if(Input.GetKeyDown(KeyCode.F))
        {
            _object.transform.SetPositionAndRotation(transform.position,transform.rotation);
        }
    }
}
