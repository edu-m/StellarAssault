using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;

    private void Start()
    {
        LoadMixer();
    }

    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
        musicSlider.value = sliderValue;
        PlayerPrefs.SetFloat("musicVolume", sliderValue);

    }

    public void SetSoundsVolume(float sliderValue)
    {
        audioMixer.SetFloat("soundsVolume", Mathf.Log10(sliderValue) * 20);
        soundsSlider.value = sliderValue;
        PlayerPrefs.SetFloat("soundsVolume", sliderValue);

    }

    private void LoadMixer()
    {   
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
            musicSlider.value = 1f;

        }
        if (!PlayerPrefs.HasKey("soundsVolume"))
        {
            PlayerPrefs.SetFloat("soundsVolume", 1f);
            soundsSlider.value = 1f;
        }
        
        SetMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        SetSoundsVolume(PlayerPrefs.GetFloat("soundsVolume"));
    }
}
