using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public static Action shootInput;
    public static Action reloadInput;
    private Animator animator;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Start()
    {
        animator = GameObject.Find("PlayerCharacter").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            shootInput?.Invoke();

        if (Input.GetKeyDown(reloadKey))
        {
            //animator.SetBool("Reloading", true);
            reloadInput?.Invoke();
            //animator.SetBool("Reloading", false);
        }

    }
}