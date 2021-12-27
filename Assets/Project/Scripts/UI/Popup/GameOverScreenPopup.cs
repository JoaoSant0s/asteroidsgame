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
    public class GameOverScreenPopup : BasePopup
    {
        public delegate void OnRestartGame();
        public static OnRestartGame RestartGame;

        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private UINavigationKeyboardMapData navigationKeyoard;

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
        
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyUp(navigationKeyoard.confirmAction))
            {
                NextAction();
            }
        }
#endif

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