using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.CommonWrapper;
using AsteroidsGame.Manager;
using UnityEngine.Events;

namespace AsteroidsGame
{
    [RequireComponent(typeof(Button))]
    public class RewardedVideoButton : MonoBehaviour
    {
        public delegate void OnShowRewardedVideo(UnityAction action);
        public static event OnShowRewardedVideo ShowRewardedVideo;

        private Button button;
        private UnityAction action;

        #region Unity Methods

        private void Awake()
        {
            button = GetComponent<Button>();
            SpaceshipSpawner.OnEnabeRewardButton += ShowButton;
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
            SpaceshipSpawner.OnEnabeRewardButton -= ShowButton;
        }

        #endregion

        #region Private Methods       
        private void ShowButton(UnityAction newAction)
        {
            if (button.gameObject.activeSelf) return;
            action = newAction;

            AdManager.Instance.WaitRewardAdsReady(() =>
            {
                EnableButton(true);
            });
        }

        private void EnableButton(bool enable)
        {
            button.gameObject.SetActive(enable);
        }

        #endregion
    }
}