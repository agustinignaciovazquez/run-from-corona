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
        
        private RewardedAd rewardedAd;
        void Start()
        {
            CreateAndLoadRewardedAd();
        }

        public void CreateAndLoadRewardedAd()
        {
            #if UNITY_ANDROID
                        string adUnitId = "ca-app-pub-1841714642549048/5405647102";
            #elif UNITY_IPHONE
                        string adUnitId = "ca-app-pub-1841714642549048/6191281012";
            #else
                        string adUnitId = "unexpected_platform";
            #endif
            
            this.rewardedAd = new RewardedAd(adUnitId);

            // Called when an ad request has successfully loaded.
            this.rewardedAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // Called when an ad request failed to load.
            this.rewardedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // Called when an ad is shown.
            this.rewardedAd.OnAdOpening += HandleRewardBasedVideoOpened;
            // Called when an ad request failed to show.
            this.rewardedAd.OnAdFailedToShow += HandleRewardBasedVideoFailShow;
            // Called when the user should be rewarded for interacting with the ad.
            this.rewardedAd.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            this.rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            this.rewardedAd.LoadAd(request);
        }
        public void ShowRewardBasedAd()
        {
            loadAdAlert.SetActive(true);
            reviveButton.interactable = false;
            
            if (rewardedAd.IsLoaded()){
                rewardedAd.Show();
            }
            else
            {
                StartCoroutine(displayCouldNotLoadAlert());
            }
        }
        
        
        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
            //rewardedAd.Show();
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdErrorEventArgs args)
        {
            StartCoroutine(displayCouldNotLoadAlert());
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
            
        }

        public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
        {
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
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
           // playerController.Resurrect();
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
            StartCoroutine(LoadNewAd());
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            //Unmute audio
            audioMixer.SetFloat("Volume", 0f);

            //RewardPlayer
            playerController.Resurrect();
            
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
            StartCoroutine(LoadNewAd());
        }

        public void HandleRewardBasedVideoFailShow(object sender, EventArgs args)
        {
            StartCoroutine(displayCouldNotLoadAlert());
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
            StartCoroutine(LoadNewAd());
        }
        
        IEnumerator displayCouldNotLoadAlert()
        {
            couldNotLoadAdAlert.SetActive(true);
        
            yield return new WaitForSeconds(0.4f);

            couldNotLoadAdAlert.SetActive(false);
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
            //CreateAndLoadRewardedAd();
        }
        
        IEnumerator LoadNewAd()
        {
            couldNotLoadAdAlert.SetActive(false);
            loadAdAlert.SetActive(false);
            reviveButton.interactable = true;
            //CreateAndLoadRewardedAd();
            yield return null;
        }
    
}
