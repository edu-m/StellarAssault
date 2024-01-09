using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
    public override void Interact()
    {
        PlayerData.Instance.Heal(10);
        gameObject.SetActive(false);
    }
}
