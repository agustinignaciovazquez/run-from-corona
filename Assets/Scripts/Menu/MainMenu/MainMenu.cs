using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void OpenShop()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("ShopMenu");
        Amplitude.Instance.logEvent("MENU_SHOP");
    }
    
    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("Loading");
        Amplitude.Instance.logEvent("MENU_PLAY_GAME");
    }

    public void OpenConfig()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("ConfigurationMenu");
        Amplitude.Instance.logEvent("MENU_CONFIG");
    }

    public void Quit()
    {
        Amplitude.Instance.logEvent("MENU_EXIT_APP");
        Application.Quit();
    }
}
