using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("character_nearby", false);
    }

    // Update is called once per frame
    private void OnTriggerEnter()
    {
        animator.SetBool("character_nearby", true);
    }
    private void OnTriggerExit()
    {
        animator.SetBool("character_nearby", false);
    }
}
