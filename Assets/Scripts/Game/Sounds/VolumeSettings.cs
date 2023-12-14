using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] public float musicSlider;
     
    public void SetMusicVolume()
    {
        mixer.SetFloat("music", musicSlider);
    }
}
