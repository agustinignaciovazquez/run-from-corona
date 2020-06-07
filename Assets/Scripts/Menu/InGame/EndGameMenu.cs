using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class EndGameMenu : MonoBehaviour
{
    private PlayerController PlayerController;

    void Awake()
    {
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Time.timeScale = 1f ;
        SceneManager.LoadScene("GameScene");
    }
    
    public void QuitToMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Continue()
    {
        PlayerController.Resurrect();
    }
    
}
