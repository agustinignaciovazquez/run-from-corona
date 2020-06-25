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
    [SerializeField] private PlayerController playerController;
    private BackgroundSettings backgroundSettings;
    private AudioManager audioManager;
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        backgroundSettings = BackgroundSettings.Instance;
    }

    private string GetCurrentBackgroundName()
    {
        return backgroundSettings.Backgrounds[playerController.BackgroundIndex].BackgroundImage.name;
    }

    private string GetFirstBackgroundName()
    {
        return backgroundSettings.Backgrounds[0].BackgroundImage.name;
    }
    public void Restart()
    {
        Amplitude.Instance.logEvent("RESTART_GAME");
        audioManager.Mute(GetCurrentBackgroundName());
        audioManager.Unmute(GetFirstBackgroundName());
        //StartCoroutine(audioManager.FadeOut(GetCurrentBackgroundName(),0.1f));
        //StartCoroutine(audioManager.FadeIn(GetFirstBackgroundName(),0.01f,0.15f));
        audioManager.Play("ButtonClick");
        Time.timeScale = 1f ;
        SceneManager.LoadScene("GameScene");
    }
    
    public void QuitToMenu()
    {
        Amplitude.Instance.logEvent("QUIT_TO_MENU");
        audioManager.Mute(GetCurrentBackgroundName());
        //StartCoroutine(audioManager.FadeOut(GetCurrentBackgroundName(),0.1f));
        audioManager.Play("ButtonClick");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void ShowOcasionalAd()
    {
        ocasionalAdsHandler.ShowRewardBasedAd();
        ocasionalAdsHandler.CreateAndLoadRewardedAd();
    }

    public void Continue()
    {
        Amplitude.Instance.logEvent("REVIVE_AD_REQUEST");
        reviveAdsHandler.ShowRewardBasedAd();
	    reviveAdsHandler.CreateAndLoadRewardedAd();
    }

}
