using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyCard : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject keyCard;
    [SerializeField] List<Transform> transforms;
    [SerializeField] GameObject keycardOverlay;
    [SerializeField] GameObject PlayerCharacter;

    public string GetDescription()
    {
        return "Pick up";
    }

    public void Interact()
    {
        PlayerData.SetKeyCard(true);
        keyCard.SetActive(false);
        keycardOverlay.SetActive(true);
    }

    // Update is called once per frame
    public void Start()
    {
        keycardOverlay.SetActive(false);
        Transform tempTransform = transforms[Random.Range(0, transforms.Count)];
        keyCard.transform.SetPositionAndRotation(tempTransform.position, tempTransform.rotation);
    }
}
