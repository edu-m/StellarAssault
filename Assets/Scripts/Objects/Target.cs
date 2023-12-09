using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{

    [SerializeField] public float health;

    public void Damage(float damage)
    {
        Debug.Log("Damage");
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}