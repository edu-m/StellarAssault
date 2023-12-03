using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Transform playerPosition;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = playerPosition.rotation;
    }
}
