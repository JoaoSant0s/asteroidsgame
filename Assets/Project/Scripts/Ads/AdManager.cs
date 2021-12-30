using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Advertisements;

using AsteroidsGame.Data;
using JoaoSant0s.CommonWrapper;
using UnityEngine.Events;

namespace AsteroidsGame
{
    public class AdManager : SingletonBehaviour<AdManager>
    {
        private AdConfigData adConfig;

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
        public void WaitRewardAdsReady(UnityAction action)
        {
            StartCoroutine(OWaitRewardAdsReadyRoutine(action));
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
            Advertisement.Show(adConfig.placementRewardedVideoId, new RewardedVideoObject(completeRewardAction));
        }

        private IEnumerator OWaitRewardAdsReadyRoutine(UnityAction action)
        {
            var rewardId = adConfig.placementRewardedVideoId;
            yield return new WaitUntil(() => Advertisement.IsReady(rewardId));

            action?.Invoke();
        }

        #endregion
    }
}
