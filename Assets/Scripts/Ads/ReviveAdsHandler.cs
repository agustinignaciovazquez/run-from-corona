using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class ReviveAdsHandler : MonoBehaviour
{
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private GameObject loadAdAlert;
        [SerializeField] private GameObject couldNotLoadAdAlert;
        [SerializeField] private Button reviveButton;
        [SerializeField] private PlayerController playerController;
        
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
            
            LoadRewardBasedAd();
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
            loadAdAlert.SetActive(true);
            reviveButton.interactable = false;
            
            if (rewardBasedVideoAd.IsLoaded()){
                rewardBasedVideoAd.Show();
            }
            else
            {
                StartCoroutine(displayCouldNotLoadAlert());
                LoadRewardBasedAd();
            }
        }
        
        
        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
            /*rewardBasedVideoAd.Show();
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;*/
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            /*StartCoroutine(displayCouldNotLoadAlert());
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;*/
            StartCoroutine(displayCouldNotLoadAlert());
            
        }

        public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
        {
           //Pause game
        }

        public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
        {
            //Mute audio
            audioMixer.SetFloat("Volume", -80f);
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
            //Unmute audio
            audioMixer.SetFloat("Volume", 0f);
            //RewardPlayer
            playerController.Resurrect();
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            //Unmute audio
            audioMixer.SetFloat("Volume", 0f);

            //RewardPlayer
            playerController.Resurrect();
            
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
        }

        public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
        {
            //RewardPlayer
            playerController.Resurrect();
        }
        
        IEnumerator displayCouldNotLoadAlert()
        {
            couldNotLoadAdAlert.SetActive(true);
        
            yield return new WaitForSeconds(0.4f);

            couldNotLoadAdAlert.SetActive(false);
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
        }
    
}
