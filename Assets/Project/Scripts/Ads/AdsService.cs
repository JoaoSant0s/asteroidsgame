using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

using JoaoSant0s.CommonWrapper;

using AsteroidsGame.Data;
using AsteroidsGame.UI;
using JoaoSant0s.ServicePackage.General;

namespace AsteroidsGame
{
    public class AdsService : Service
    {
        private AdConfigData adConfig;

        private string RewardedVideoId => adConfig.placementRewardedVideoId;

        #region Public Override Methods

        public override void OnInit()
        {
            adConfig = Resources.Load<AdConfigData>("AdConfig");
            StartUnityAds();
        }

        #endregion

        #region Public Methods
        public Coroutine WaitRewardAdsReady(UnityAction action)
        {
            return StartCoroutine(OWaitRewardAdsReadyRoutine(action));
        }

        public void ShowRewardedVideo(UnityAction completeRewardAction)
        {
            Advertisement.Show(RewardedVideoId, new RewardedVideoObject(completeRewardAction));
        }

        #endregion

        #region Private Methods

        private void StartUnityAds()
        {
#if UNITY_ANDROID
            Advertisement.Initialize(adConfig.playStoreGameId);
#endif
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
