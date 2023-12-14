using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    AudioSource audioSource;
    [SerializeField] private AudioClip stealthModeMusic;
    [SerializeField] private AudioClip directModeMusic;
    // game can be played both in "stealth mode" and "direct mode"
    // the music will change accordingly
    [SerializeField] public bool directMode;
    IEnumerator ChangeMusic()
    {
        //play stealth music
        audioSource.clip = stealthModeMusic;
        audioSource.Play();
        yield return new WaitUntil(() => directMode);
       // play direct music
        audioSource.clip = directModeMusic;
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        directMode = false;
        StartCoroutine(ChangeMusic());
    }
}
