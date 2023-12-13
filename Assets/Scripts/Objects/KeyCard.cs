using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    [SerializeField] GameObject keyCard;
    // Update is called once per frame
    private void OnTriggerEnter()
    {
        PlayerData.SetKeyCard(true);
        keyCard.SetActive(false);
    }
}
