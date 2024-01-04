using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        health = maxHealth;
    }

    public static bool HasKeyCard() => hasKeyCard;

    public static void SetKeyCard(bool value) => hasKeyCard = value;

    public void StartAgain()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void Damage(float damage)
    {
#if true
        return;
#endif
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
