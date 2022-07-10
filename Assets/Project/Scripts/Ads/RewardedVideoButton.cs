using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using AsteroidsGame.Manager;

namespace AsteroidsGame.UI
{
    [RequireComponent(typeof(Button))]
    public class RewardedVideoButton : MonoBehaviour
    {
        public static event Action<UnityAction> ShowRewardedVideo;

        private Button button;
        private UnityAction action;

        private Coroutine waitForAdsRoutine;

        #region Unity Methods

        private void Awake()
        {
            button = GetComponent<Button>();
            SpaceshipSpawner.OnEnabeRewardButton += EnableRewardButton;
            EnableButton(false);
        }

        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                ShowRewardedVideo?.Invoke(action);
                EnableButton(false);
            });
        }

        private void OnDestroy()
        {
            SpaceshipSpawner.OnEnabeRewardButton -= EnableRewardButton;
        }

        #endregion

        #region Private Methods       
        private void EnableRewardButton(bool enable, UnityAction newAction)
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

        private void TryEnableButton(UnityAction newAction)
        {
            action = newAction;

            waitForAdsRoutine = AdManager.Instance.WaitRewardAdsReady(() =>
            {
                EnableButton(true);
            });
        }

        private void DisableButton()
        {
            EnableButton(false);
            action = null;
            if (waitForAdsRoutine == null) return;

            AdManager.Instance.StopCoroutine(waitForAdsRoutine);
            waitForAdsRoutine = null;
        }

        private void EnableButton(bool enable)
        {
            button.gameObject.SetActive(enable);
        }

        #endregion
    }
}