using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    private static PlayerData _instance;
    int health;
    public const int maxHealth = 100;
    [SerializeField] Slider lifeBar;
    private static bool hasObject;
    // Start is called before the first frame update

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public static PlayerData Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerData>();
            return _instance;
        }
    }
    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        lifeBar.value = health;
    }

    public static bool HasObject() => hasObject;

    public static void SetObject(bool value) => hasObject = value;

    public void Heal(int amount)
    {
        if (health + amount <= maxHealth)
            health += amount;
        else health = maxHealth;
    }
}
