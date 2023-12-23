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
    // Update is called once per frame
    public void Start()
    {
        keycardOverlay.SetActive(false);
        Transform tempTransform = transforms[Random.Range(0, transforms.Count)];
        keyCard?.transform.SetPositionAndRotation(tempTransform.position, tempTransform.rotation);
    }
    private void OnTriggerEnter()   
    {
        //Debug.Log("On Enter Keycard Collision");
        PlayerData.SetKeyCard(true);
        keyCard?.SetActive(false);
        keycardOverlay?.SetActive(true);
    }
}
