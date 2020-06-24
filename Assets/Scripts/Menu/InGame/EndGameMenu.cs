using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private ReviveAdsHandler reviveAdsHandler;
    [SerializeField] private OcasionalAdsHandler ocasionalAdsHandler;
    void Start()
    {
    
    }
    
    public void Restart()
    {
        //TODO
        //StartCoroutine(audioManager.FadeOut("x",0.01f,0.15f));
        //StartCoroutine(audioManager.FadeIn("Airport",0.01f,0.15f));
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Time.timeScale = 1f ;
        SceneManager.LoadScene("GameScene");
    }
    
    public void QuitToMenu()
    {
        //TODO
        //StartCoroutine(audioManager.FadeOut("x",0.01f,0.15f));
        
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void ShowOcasionalAd()
    {
        ocasionalAdsHandler.ShowRewardBasedAd();
    }

    public void Continue()
    {
        reviveAdsHandler.ShowRewardBasedAd();
    }

}
