using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigurationMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    [SerializeField] private Slider[] sliders;

    void Start()
    {
        sliders[0].value = PlayerPrefs.GetFloat("Volume");
        sliders[1].value = PlayerPrefs.GetFloat("MusicVolume");
        sliders[2].value = PlayerPrefs.GetFloat("SoundEffectsVolume");   
    }
    
    
    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("Menu");
    }

    public void SetMasterVolume(float volume)
    {
        AudioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SoundEffectsVolume(float volume)
    {
        AudioMixer.SetFloat("SoundEffectsVolume", volume);
        PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
    }

    
}
