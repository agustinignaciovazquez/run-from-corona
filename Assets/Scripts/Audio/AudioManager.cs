using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    public static AudioManager instance;
    
    public AudioMixer audioMixer;
    private static bool keepFadeIn;
    private static bool keepFadeOut;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.AudioMixerGroup;
        }
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("MuteMusic") == 0)
        {
            Play("Airport");
        }
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    
    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.mute = true;
    }
    
    public void Unmute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.mute = false;
    }

    public void MuteAudioMixerGroupMusic()
    {
        audioMixer.SetFloat("MusicVolume", -80f);
    }
    
    public void UnmuteAudioMixerGroupMusic()
    {
        audioMixer.SetFloat("MusicVolume", 0f);
    }

    
    
    public IEnumerator FadeIn(string name, float speed, float maxVolume)
    {
        keepFadeIn = true;
        keepFadeOut = false;
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.volume = 0;
        print("FADE IN A "+name);
        float audioVolume = s.volume;
        while (s.volume < maxVolume && keepFadeIn)
        {
            //Debug.Log("VOLUME= " + audioVolume);
            audioVolume += speed;
            s.volume = audioVolume;
            yield return null;
        }
        
    }
   
    public IEnumerator FadeOut(string name, float speed)
    {
        keepFadeIn = false;
        keepFadeOut = true;
        print("FADE OUT A "+name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float audioVolume = s.volume;
        while (s.volume >= speed && keepFadeOut)
        {
            audioVolume -= speed;
            s.volume = audioVolume;
            yield return null;
        }
        s.source.Stop();

    }

    
}
