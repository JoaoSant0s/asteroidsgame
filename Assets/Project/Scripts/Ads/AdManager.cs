using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

using JoaoSant0s.CommonWrapper;

using AsteroidsGame.Data;
using AsteroidsGame.UI;


namespace AsteroidsGame
{
    public class AdManager : SingletonBehaviour<AdManager>
    {
        private AdConfigData adConfig;

        private string RewardedVideoId => adConfig.placementRewardedVideoId;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            adConfig = Resources.Load<AdConfigData>("AdConfig");
        }

        private void Start()
        {
            StartUnityAds();
        }

        private void OnDestroy()
        {
            RewardedVideoButton.ShowRewardedVideo -= ShowRewardedVideo;
        }

        #endregion

        #region Public Methods
        public Coroutine WaitRewardAdsReady(UnityAction action)
        {
            return StartCoroutine(OWaitRewardAdsReadyRoutine(action));
        }

        public bool IsRewardVideoReady()
        {
            return IsAdvertisementReady(RewardedVideoId);
        }

        #endregion

        #region Private Methods

        private void StartUnityAds()
        {
#if UNITY_ANDROID
            Advertisement.Initialize(adConfig.playStoreGameId);
#endif

            RewardedVideoButton.ShowRewardedVideo += ShowRewardedVideo;
        }

        private void ShowRewardedVideo(UnityAction completeRewardAction)
        {
            Advertisement.Show(RewardedVideoId, new RewardedVideoObject(completeRewardAction));
        }

        private IEnumerator OWaitRewardAdsReadyRoutine(UnityAction action)
        {
            yield return new WaitUntil(() => IsAdvertisementReady(RewardedVideoId));

            action?.Invoke();
        }

        private bool IsAdvertisementReady(string placementId)
        {
            return Advertisement.IsReady(placementId);
        }

        #endregion
    }
}
