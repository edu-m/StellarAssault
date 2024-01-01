using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject bombOverlay;

    public string GetDescription()
    {
        return "Place Bomb";
    }

    public void Interact()
    {
        PlayerData.SetObject(false);
        bomb.SetActive(true);
        bombOverlay.SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Start()
    {
        PlayerData.SetObject(true);
        bombOverlay.SetActive(true);
    }
}
