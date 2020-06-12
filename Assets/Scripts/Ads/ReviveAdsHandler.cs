using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Audio;


public class ReviveAdsHandler : MonoBehaviour
{
        [SerializeField] private AudioMixer audioMixer;
        private PlayerController playerController;
        
        private RewardBasedVideoAd rewardBasedVideoAd;
        void Start(){
            
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            
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
            string adUnitId = "ca-app-pub-1841714642549048/5405647102";
            #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-1841714642549048/6191281012";
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
                print("SE MOSTRO");
            }
            else
            {
                print("NO SE CARGO");
            }
        }
        
        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
           
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
           //TODO SHOW ERROR LOADING AD
        }

        public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
        {
           //Pause game
        }

        public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
        {
            //Mute audio
            audioMixer.SetFloat("Volume", -80f);
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
            //Load new add
            this.LoadRewardBasedAd();
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
            print("User rewarded with: " + amount + " " + type);
            playerController.Resurrect();
        }

        public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
        {
            
        }
        
    
}
