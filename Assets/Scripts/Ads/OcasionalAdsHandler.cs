using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OcasionalAdsHandler : MonoBehaviour
{
    private InterstitialAd interstitial;

    void Start()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-1841714642549048~7874724311";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-1841714642549048~8947653900";
        #else
                string adUnitId = "unexpected_platform";
        #endif
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        RequestInterstitial();
    }

    private void RequestInterstitial() {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this.interstitial.LoadAd(request);
    }
    public void ShowRewardBasedAd()
    {
        if(interstitial.IsLoaded())
            interstitial.Show();
        else
        {
            RequestInterstitial();
        }
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        
    }
    
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //RequestInterstitial();
    }
        
    
}
