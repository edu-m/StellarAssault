using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour, IInteractable
{
    [SerializeField] bool shouldHaveObject;
    public string GetDescription()
    {
        return "Interact";
    }
    public void Interact()
    {
        if (PlayerData.HasObject() == shouldHaveObject)
            InteractWithConstraint();
    }

    public void InteractWithConstraint() => GameManager.Instance.NextLevel();


}
