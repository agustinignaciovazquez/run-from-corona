using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetter : MonoBehaviour
{
    [SerializeField] private Button muteButton;
    [SerializeField] private Button muteMusicButton;

    [SerializeField] private AudioMixer audioMixer;

    private AudioManager audioManager;

    private bool firstTime = true;

    public void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        audioMixer.SetFloat("SoundEffectsVolume", PlayerPrefs.GetFloat("SoundEffectsVolume"));
        
        if (PlayerPrefs.GetInt("MuteSound") == 1)
        {
            AudioListener.pause = true;
        }
        else
        {
            audioManager.Unmute("Airport");
            StartCoroutine(audioManager.FadeIn("Airport", 0.01f, 0.15f));
        }
        
        if (PlayerPrefs.GetInt("MuteMusic") == 1)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
        }

    }
    
    public void ToggleSound()
    {
        AudioListener.pause = !AudioListener.pause;
        ToggleIndicator();
    }

    
    public void MuteMusic()
    {
        
        if (PlayerPrefs.GetInt("MuteMusic") == 0)
        {
            audioManager.MuteAudioMixerGroupMusic();
            PlayerPrefs.SetInt("MuteMusic", 1);
            muteMusicButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            if (firstTime)
            {   
                audioManager.Play("Airport");
                firstTime = false;
            }
            audioManager.UnmuteAudioMixerGroupMusic();
            PlayerPrefs.SetInt("MuteMusic", 0);
            muteMusicButton.transform.GetChild(0).gameObject.SetActive(false);
        }
        
        
    }
    
    public void ToggleIndicator()
    {
        if (!AudioListener.pause){
            muteButton.transform.GetChild(0).gameObject.SetActive(false);
            PlayerPrefs.SetInt("MuteSound",0);
        }
        else{
            muteButton.transform.GetChild(0).gameObject.SetActive(true);
            PlayerPrefs.SetInt("MuteSound",1);
        }
        
    }

   
    
}
