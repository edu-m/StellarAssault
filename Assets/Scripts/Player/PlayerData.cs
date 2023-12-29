using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
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
        GetShot();
    }

    public static bool HasKeyCard() => hasKeyCard;

    public static void SetKeyCard(bool value) => hasKeyCard = value;

    public void GetShot()
    {
        health -= 10;
        lifeBar.value = health;
    }
}
