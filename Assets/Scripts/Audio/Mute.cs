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
            Debug.Log("ACA " + PlayerPrefs.GetInt("MuteSound"));
            AudioListener.pause = true;
        }
        else
        {
            Debug.Log("ACA " + PlayerPrefs.GetInt("MuteSound"));
        }
       
    }
    
    public void ToggleSound()
    {
        AudioListener.pause = !AudioListener.pause;
        ToggleIndicator(muteButton);
    }

    
    public void MuteMusic()
    {
        PlayerPrefs.SetInt("MuteMusic", 0);
    }
    
    public void ToggleIndicator(Button button)
    {
        if (!AudioListener.pause){
            button.transform.GetChild(0).gameObject.SetActive(false);
            PlayerPrefs.SetInt("MuteSound",0);
        }
        else{
            button.transform.GetChild(0).gameObject.SetActive(true);
            PlayerPrefs.SetInt("MuteSound",1);
        }
        
    }
    
}
