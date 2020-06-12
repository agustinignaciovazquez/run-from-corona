using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Audio;


public class GemAdsHandler : MonoBehaviour
{
        [SerializeField] private AudioMixer audioMixer;
        
        private PlayerItemsState playerItemsState;
        private RewardBasedVideoAd rewardBasedVideoAd;
       
        void Start(){
            
            playerItemsState = PlayerItemsState.Instance;
            
            rewardBasedVideoAd = RewardBasedVideoAd.Instance;
            
            rewardBasedVideoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
      
            rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

            rewardBasedVideoAd.OnAdOpening += HandleRewardBasedVideoOpened;

            rewardBasedVideoAd.OnAdStarted += HandleRewardBasedVideoStarted;

            rewardBasedVideoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;

            rewardBasedVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;

            rewardBasedVideoAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
            
            this.LoadRewardBasedAd();
        }
      
        
        public void LoadRewardBasedAd(){
            #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-1841714642549048/7662492394";
            #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-1841714642549048/4234862201";
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
           audioMixer.SetFloat("Volume", -80f);
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
            //Back to the end game menu
            //Unmute audio
            audioMixer.SetFloat("Volume", 0f);
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            //Unmute audio
            audioMixer.SetFloat("Volume", 0f);
            //RewardPlayer
            string type = args.Type;
            double amount = args.Amount;
            playerItemsState.AddSaveGems((int)amount);
            print("User rewarded with: " + amount + " " + type);
        }

        public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
        {
            
        }
        
    
}
