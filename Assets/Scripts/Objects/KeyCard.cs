using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyCard : MonoBehaviour
{
    [SerializeField] GameObject keyCard;
    [SerializeField] List<Transform> transforms;
    [SerializeField] GameObject keycardOverlay;
    [SerializeField] GameObject PlayerCharacter;
    private Animator animator;

    public string GetDescription()
    {
        return "Pick up";
    }

    public void Interact()
    {
        animator.SetBool("Pickup", true);
        PlayerData.SetKeyCard(true);
        keyCard.SetActive(false);
        keycardOverlay.SetActive(true);
    }

    // Update is called once per frame
    public void Start()
    {
        animator = PlayerCharacter.GetComponent<Animator>();
        keycardOverlay.SetActive(false);
        Transform tempTransform = transforms[Random.Range(0, transforms.Count)];
        keyCard.transform.SetPositionAndRotation(tempTransform.position, tempTransform.rotation);
    }
}
