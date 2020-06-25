using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OcasionalAdsHandler : MonoBehaviour
{
        
        private InterstitialAd rewardedAd;
        void Start()
        {
            CreateAndLoadRewardedAd();
        }

        public void CreateAndLoadRewardedAd()
        {
            #if UNITY_ANDROID
                        string adUnitId = "ca-app-pub-1841714642549048/5797728452";
            #elif UNITY_IPHONE
                        string adUnitId = "ca-app-pub-1841714642549048/1692916190";
            #else
                        string adUnitId = "unexpected_platform";
            #endif
            
            this.rewardedAd = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            this.rewardedAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // Called when an ad request failed to load.
            //this.rewardedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // Called when an ad is shown.
            this.rewardedAd.OnAdOpening += HandleRewardBasedVideoOpened;
            // Called when an ad request failed to show.
            //this.rewardedAd.OnAdFailedToShow += HandleRewardBasedVideoFailShow;
            // Called when the user should be rewarded for interacting with the ad.
            //this.rewardedAd.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            this.rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            this.rewardedAd.LoadAd(request);
        }
        public void ShowRewardBasedAd()
        {
            if (rewardedAd.IsLoaded()){
                rewardedAd.Show();
            }
        }
        
        
        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
            
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdErrorEventArgs args)
        {
          
        }

        public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
        {
       
        }

        public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
        {
           
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
        
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
          
        }

        public void HandleRewardBasedVideoFailShow(object sender, EventArgs args)
        {
            
        }
        
    
}
