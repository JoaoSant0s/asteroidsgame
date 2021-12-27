using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;
using AsteroidsGame.Data;

namespace AsteroidsGame.UI.Popup
{
    public class SplashScreenPopup : BasePopup
    {
        [SerializeField]
        private Button startButton;

        [SerializeField]
        private UINavigationKeyboardMapData navigationKeyoard;

        [SerializeField]
        private FlagAsset enableGameplayFlag;

        private FlagService flagService;

        #region Unity Methods
        private void Start()
        {
            flagService = Services.Get<FlagService>();
            SetButtonEvents();
        }

        private void Update()
        {
            if (Input.GetKeyUp(navigationKeyoard.confirmAction))
            {
                NextAction();
            }
        }

        #endregion

        #region Private Methods
        private void SetButtonEvents()
        {
            startButton.onClick.AddListener(() =>
            {
                NextAction();
            });
        }

        private void NextAction()
        {
            flagService.Raise(enableGameplayFlag);
            Hide();
        }

        #endregion
    }
}