using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPowerup : Powerup
{
    [SerializeField] float multiplier = 1.5f;
    protected override void PowerupOperationBeforeTimerWait() => Movement.Instance.sprintSpeed *= multiplier;
    protected override void PowerupOperationAfterTimerWait() => Movement.Instance.sprintSpeed /= multiplier;
}
