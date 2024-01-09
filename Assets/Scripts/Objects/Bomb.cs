using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject bombOverlay;
    [SerializeField] AudioSource bombAudioSource;

    public string GetDescription()
    {
        return "Place Bomb";
    }

    IEnumerator PlayAndDeactivate() {
        bombAudioSource.Play();
        yield return new WaitForSeconds(bombAudioSource.clip.length);
        PlayerData.SetObject(false);
        bomb.SetActive(true);
        bombOverlay.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Interact()
    {
        StartCoroutine(PlayAndDeactivate());
    }

    // Update is called once per frame
    public void Start()
    {
        //bombAudioSource = GetComponent<AudioSource>();  
        PlayerData.SetObject(true);
        bombOverlay.SetActive(true);
    }
}
