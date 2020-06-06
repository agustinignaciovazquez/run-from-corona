using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
        private RewardBasedVideoAd rewardBasedVideoAd;
        void Start(){
            
            rewardBasedVideoAd = RewardBasedVideoAd.Instance;
            
            
            rewardBasedVideoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
      
            rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

            rewardBasedVideoAd.OnAdOpening += HandleRewardBasedVideoOpened;

            rewardBasedVideoAd.OnAdStarted += HandleRewardBasedVideoStarted;

            rewardBasedVideoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;

            rewardBasedVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;

            rewardBasedVideoAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
            
            
        }
      
        
        public void LoadRewardBasedAd(){
            #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
            #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
            #else
            string adUnitId = "unexpected_platform";
            #endif

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            
            // Load the rewarded video ad with the request.
            this.rewardBasedVideoAd.LoadAd(request, adUnitId);
        }

        public void ShowRewardBasedAd()
        {
            if (rewardBasedVideoAd.IsLoaded()){
                rewardBasedVideoAd.Show();
            }
        }
        
        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
           
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
           //Try reload
        }

        public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
        {
           //Pause game
        }

        public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
        {
           //Mute Audio
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
            //Back to the end game menu
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            //Reward the user. Continue, for example
        }

        public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
        {
            
        }
        
    
}
