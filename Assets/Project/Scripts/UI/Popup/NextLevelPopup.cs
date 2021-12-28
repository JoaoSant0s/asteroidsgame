using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Flag;
using AsteroidsGame.Data;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;

namespace AsteroidsGame.UI.Popup
{
    public class NextLevelPopup : BasePopup
    {
        [Header("Components")]
        [SerializeField]
        private Button continueButton;
        [SerializeField]
        private TextMeshProUGUI levelLabel;

        [Header("Data")]
        [SerializeField]
        private UINavigationKeyboardMapData navigationKeyoard;

        [SerializeField]
        private FlagAsset enableGameplayFlag;
        private FlagService flagService;

        private UnityAction action;

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

        #region Public Methods

        public void SetVisual(int visualLevel)
        {
            levelLabel.text = string.Format("Level {0}", visualLevel);
        }
        public void SetGoAction(UnityAction action)
        {
            this.action = action;
        }

        #endregion

        #region  Private Methods
        private void SetButtonEvents()
        {
            continueButton.onClick.AddListener(() =>
            {
                NextAction();
            });
        }

        private void NextAction()
        {
            flagService.Raise(enableGameplayFlag);
            action?.Invoke();
            Hide();
        }

        #endregion
    }
}
