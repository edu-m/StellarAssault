using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour,IDamageable
{
    int health;
    const int  maxHealth = 100;
    [SerializeField] Slider lifeBar;
    Transform startingPosition;

    private static bool hasKeyCard;
    // Start is called before the first frame update
    void Start()
    {
        hasKeyCard = false;
        health = 100;
        startingPosition = GetComponent<Transform>();
        Debug.Log("Starting position at " + startingPosition.position);
    }

    private void Update()
    {
       
    }

    public static bool HasKeyCard() => hasKeyCard;

    public static void SetKeyCard(bool value) => hasKeyCard = value;

    public void StartAgain()
    {
        Debug.Log("Start Again");
        Debug.Log("Actual position " + transform.position + " Start Position " + startingPosition.position);
        transform.position = startingPosition.position;
        
        hasKeyCard = false;
        health = 100;

    }
    public void Damage(float damage)
    {
        health -= (int)damage;
        lifeBar.value = health;
        if(health<=0)
            DeathEvent();
    }
    public void DeathEvent()
    {
        Debug.Log("Player is dead");
        StartAgain();
    }


}
