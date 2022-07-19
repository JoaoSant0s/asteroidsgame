using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;
using AsteroidsGame.Data;
using AsteroidsGame.Manager;
using AsteroidsGame.Save;

namespace AsteroidsGame.UI.Popup
{
    public class SplashScreenPopup : BasePopup
    {
        [Header("Components")]
        [SerializeField]
        private Button newRunButton;

        [SerializeField]
        private Button continueButton;

        [Header("Data")]

        [SerializeField]
        private FlagAsset enableGameplayFlag;

        private FlagService flagService;
        private PlayerPersistenceService playerPersistence;

        #region Unity Methods
        private void Start()
        {
            flagService = Services.Get<FlagService>();
            playerPersistence = Services.Get<PlayerPersistenceService>();

            EnableButtons();
            SetButtonEvents();
        }

        #endregion

        #region Private Methods

        private void EnableButtons()
        {
            if (!playerPersistence.ContainsPlayerSave())
            {
                continueButton.gameObject.SetActive(false);
            }
        }

        private void SetButtonEvents()
        {
            newRunButton.onClick.AddListener(() =>
            {
                playerPersistence.CreatePlayerSave();
                NextAction();
            });

            continueButton.onClick.AddListener(() =>
            {
                NextAction();
            });
        }

        private void NextAction()
        {
            flagService.Raise(enableGameplayFlag);
            Close();
        }

        #endregion
    }
}