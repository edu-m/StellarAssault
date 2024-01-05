using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] public float health;

    public void Damage(int damage)
    {
        damage = (int)Mathf.Ceil(damage * (0.3f * (PlayerPrefs.GetInt("Difficulty") + 1)));
        Debug.Log("Damage is " + damage);
        health -= damage;
        if (health <= 0)
            DeathEvent();
    }

    public void DeathEvent()
    {
        Destroy(gameObject);
    }

}