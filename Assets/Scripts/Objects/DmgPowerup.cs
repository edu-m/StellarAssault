using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgPowerup : Powerup
{
    [SerializeField] float multiplier = 1.5f;
    protected override void PowerupOperationBeforeTimerWait() => Gun._instance.gunDamageMultiplier *= multiplier;
    protected override void PowerupOperationAfterTimerWait() => Gun._instance.gunDamageMultiplier /= multiplier;
}
