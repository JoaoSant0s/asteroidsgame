using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;


namespace AsteroidsGame.UI.Popup
{
    public class GameOverScreenPopup : BasePopup
    {
        public static event Action RestartGame;

        [Header("Components")]

        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private TextMeshProUGUI messageLabel;

        [Header("Data")]

        [SerializeField]
        private FlagAsset enableGameplayFlag;
        private FlagService flagService;

        #region Unity Methods
        private void Start()
        {
            flagService = Services.Get<FlagService>();
            flagService.Lower(enableGameplayFlag);

            SetButtonEvents();
        }

        #endregion

        #region Public Methods

        public void UpdateMessage(int returnLevel)
        {
            var pluralMessage = (returnLevel > 1) ? "waves" : "wave";

            messageLabel.text = string.Format("Returning {0} {1}", returnLevel, pluralMessage);
        }

        #endregion

        #region  Private Methods
        private void SetButtonEvents()
        {
            restartButton.onClick.AddListener(() =>
            {
                NextAction();
            });
        }

        private void NextAction()
        {
            RestartGame?.Invoke();
            flagService.Raise(enableGameplayFlag);
            Hide();
        }

        #endregion
    }
}