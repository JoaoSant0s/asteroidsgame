using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

using JoaoSant0s.ServicePackage.General;

using AsteroidsGame.Ads.Data;
using JoaoSant0s.CommonWrapper;
using System;

namespace AsteroidsGame.Ads
{

    public enum AdsResult
    {
        Failed = 0,
        Skipped = 1,
        Finished = 2
    }
    public class AdsService : Service, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
    {
        private AdConfigData config;
        private Dictionary<string, bool> unityAdsLoaded;
        private Dictionary<string, Action<AdsResult>> callbackActions;
        private bool initialized;

        private string RewardedVideoId => config.placementRewardedVideoId;

        #region Public Override Methods

        public override void OnInit()
        {
            config = Resources.Load<AdConfigData>("GameConfigs/AdConfig");
            unityAdsLoaded = new Dictionary<string, bool>();
            callbackActions = new Dictionary<string, Action<AdsResult>>();

            StartUnityAds();
        }

        #endregion

        #region Implemented Methods

        public void OnInitializationComplete()
        {
            initialized = true;
            LoadAllAds();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debugs.Log("OnInitializationFailed", error, message);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            unityAdsLoaded.Add(placementId, true);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debugs.Log("OnUnityAdsFailedToLoad", placementId.ToString(), error, message);
            unityAdsLoaded.Add(placementId, false);

            Debugs.Log("Trying Load again", placementId.ToString());
            LoadAdvertisement(placementId);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            TryRunCallback(placementId, AdsResult.Failed);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debugs.Log("OnUnityAdsShowStart", placementId);
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debugs.Log("OnUnityAdsShowClick", placementId);
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            TryRunCallback(placementId, (showCompletionState == UnityAdsShowCompletionState.SKIPPED) ? AdsResult.Skipped : AdsResult.Finished);
        }

        #endregion

        #region Public Methods
        public Coroutine WaitRewardAdsReady(UnityAction action)
        {
            return StartCoroutine(OWaitRewardAdsReadyRoutine(action));
        }

        public void ShowRewardedVideo(Action<AdsResult> callbackAction)
        {
            callbackActions.Add(RewardedVideoId, callbackAction);
            unityAdsLoaded.Remove(RewardedVideoId);

            Advertisement.Show(RewardedVideoId, this);
        }

        #endregion

        #region Private Methods

        private void StartUnityAds()
        {
#if UNITY_ANDROID
            Advertisement.Initialize(config.playStoreGameId, this.config.testMode, this);
#endif
        }

        private void LoadAllAds()
        {
            LoadAdvertisement(RewardedVideoId);
        }

        private void LoadAdvertisement(string placementId)
        {
            Advertisement.Load(RewardedVideoId, this);
        }

        private IEnumerator OWaitRewardAdsReadyRoutine(UnityAction action)
        {
            yield return new WaitUntil(() => initialized && IsAdvertisementReady(RewardedVideoId));

            action?.Invoke();
        }

        private bool IsAdvertisementReady(string placementId)
        {
            return unityAdsLoaded.ContainsKey(placementId) && unityAdsLoaded[placementId];
        }

        private void TryRunCallback(string placementId, AdsResult result)
        {
            LoadAdvertisement(placementId);
            if (!callbackActions.ContainsKey(placementId)) return;
            callbackActions[placementId]?.Invoke(result);
            callbackActions.Remove(placementId);
        }


        #endregion


    }
}
