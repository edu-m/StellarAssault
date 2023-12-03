using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    public GameObject _object;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Vector3 pos = transform.position;
            pos.y += 3.5f;
            Object.Instantiate(_object,pos,transform.rotation);
        }
    }
}
