using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Popup;
using UnityEngine.UI;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;

namespace AsteroidsGame.UI.Popup
{
    public class PausePopup : BasePopup
    {
        [Header("Components")]
        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private FlagAsset enableGameplayFlag;
        private FlagService flagService;

        #region Unity Methods
        private void Start()
        {
            flagService = Services.Get<FlagService>();
            flagService.Lower(enableGameplayFlag);

            SetButtonEvents();
            Time.timeScale = 0;
        }

        #endregion

        #region  Private Methods
        private void SetButtonEvents()
        {
            resumeButton.onClick.AddListener(() =>
            {
                NextAction();
            });
        }

        private void NextAction()
        {
            flagService.Raise(enableGameplayFlag);
            Hide();
            Time.timeScale = 1;
        }

        #endregion
    }
}