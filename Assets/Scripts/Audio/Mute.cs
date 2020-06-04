using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    [SerializeField] private Button muteButton;
    [SerializeField] private Button muteMusicButton;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("MuteSound") == 1)
        {
            AudioListener.pause = true;
        }

        if (PlayerPrefs.GetInt("MuteMusic") == 1)
        {
            FindObjectOfType<AudioManager>().Mute("Theme");
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
            FindObjectOfType<AudioManager>().Mute("Theme");
            PlayerPrefs.SetInt("MuteMusic", 1);
            muteMusicButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            FindObjectOfType<AudioManager>().Unmute("Theme");
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
