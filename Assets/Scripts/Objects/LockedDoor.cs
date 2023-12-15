using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] GameObject door;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = door.GetComponent<Animator>();    
    }

    // Update is called once per frame
    private void OnTriggerEnter()
    {
        if(PlayerData.HasKeyCard())
            animator.SetBool("character_nearby", true);
    }
    private void OnTriggerExit()
    {
        animator.SetBool("character_nearby", false);
    }
}
