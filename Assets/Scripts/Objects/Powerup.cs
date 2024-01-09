using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Use";
    }

    public abstract void Interact();
}
