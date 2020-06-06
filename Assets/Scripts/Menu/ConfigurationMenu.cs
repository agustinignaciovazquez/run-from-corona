using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ConfigurationMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    
    // Start is called before the first frame update
    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("Menu");
    }

    public void SetMasterVolume(float volume)
    {
        AudioMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("MusicVolume", volume);
    }

    public void SoundEffectsVolume(float volume)
    {
        AudioMixer.SetFloat("SoundEffectsVolume", volume);
    }

    
}
