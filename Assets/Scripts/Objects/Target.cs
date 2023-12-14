using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] public float health;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
            DeathEvent();
    }

    public void DeathEvent()
    {
        Destroy(gameObject);
    }

}