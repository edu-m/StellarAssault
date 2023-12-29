using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    public static MusicManager Instance
    {
        get
        {   
            if (_instance == null)
            {   //Se il MusicManager non è stato istanziato, lo ricerca nella gerarchia e lo istanzia
                _instance = GameObject.FindObjectOfType<MusicManager>();

            }
            if (_instance == null)
            {   //Se è ancora non istanziato, lo segnala
                Debug.LogError("No GameManager in scene");

            }
            return _instance;
        }
    }
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
    private void Awake()
    {
        if (_instance == null)
        {   //Se non è presente, lo istanzia e fa in modo che non venga distrutto a un eventuale caricamento
            //di una nuova scena
            _instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {   //Altrimenti, distrugge questa istanza
            GameObject.Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        directMode = false;
        StartCoroutine(ChangeMusic());
    }

   
}
