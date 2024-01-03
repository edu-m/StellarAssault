using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour,IDamageable
{
    int health;
    const int  maxHealth = 100;
    [SerializeField] Slider lifeBar;

    private static bool hasKeyCard;
    // Start is called before the first frame update
    void Start()
    {
        hasKeyCard = false;
        health = 100;
    }

    private void Update()
    {
       
    }

    public static bool HasKeyCard() => hasKeyCard;

    public static void SetKeyCard(bool value) => hasKeyCard = value;

    public void Damage(float damage)
    {
        health -= (int)damage;
        lifeBar.value = health;
        DeathEvent();
    }
    public void DeathEvent()
    {
        if (health <= 0)
        {
            Debug.Log("Player is dead");
        }
        else
            return;
    }
}
