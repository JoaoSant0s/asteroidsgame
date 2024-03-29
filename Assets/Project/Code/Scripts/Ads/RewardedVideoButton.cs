using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using JoaoSant0s.ServicePackage.General;

using AsteroidsGame.Spaceships;

namespace AsteroidsGame.Ads.UI.Inputs
{
    [RequireComponent(typeof(Button))]
    public class RewardedVideoButton : MonoBehaviour
    {
        public static event Action ShowRewardedVideo;

        private Button button;
        private Action<AdsResult> callbackAction;

        private AdsService adsService;

        private Coroutine waitForAdsRoutine;

        #region Unity Methods

        private void Awake()
        {
            this.adsService = Services.Get<AdsService>();

            this.button = GetComponent<Button>();
            SpaceshipSpawner.OnEnabeRewardButton += EnableRewardButton;
            EnableButton(false);
        }

        private void Start()
        {
            this.button.onClick.AddListener(() =>
            {
                this.adsService.ShowRewardedVideo(this.callbackAction);
                ShowRewardedVideo?.Invoke();
                EnableButton(false);
            });
        }

        private void OnDestroy()
        {
            SpaceshipSpawner.OnEnabeRewardButton -= EnableRewardButton;
        }

        #endregion

        #region Private Methods       
        private void EnableRewardButton(bool enable, Action<AdsResult> newAction)
        {
            if (enable)
            {
                TryEnableButton(newAction);
            }
            else
            {
                DisableButton();
            }
        }

        private void TryEnableButton(Action<AdsResult> newAction)
        {
            this.callbackAction = newAction;

            this.waitForAdsRoutine = this.adsService.WaitRewardAdsReady(() =>
            {
                EnableButton(true);
            });
        }

        private void DisableButton()
        {
            EnableButton(false);
            this.callbackAction = null;
            if (this.waitForAdsRoutine == null) return;

            this.adsService.StopCoroutine(this.waitForAdsRoutine);
            this.waitForAdsRoutine = null;
        }

        private void EnableButton(bool enable)
        {
            this.button.gameObject.SetActive(enable);
        }

        #endregion
    }
}