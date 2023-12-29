using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Interact";
    }

    public void Interact()
    {
        GameManager.Instance.PlayGame();
    }
}
