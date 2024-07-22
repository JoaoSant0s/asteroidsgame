using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;
using UnityEngine.InputSystem;


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

        private InputControls input;

        #region Unity Methods

        private void Awake()
        {
            input = new InputControls();
        }
        private void OnEnable()
        {
            input.UI.Enable();

            input.UI.Continue.performed += SelectionButton;
        }
        private void Start()
        {
            flagService = Services.Get<FlagService>();
            flagService.Lower(enableGameplayFlag);

            SetButtonEvents();
        }

        private void OnDisable()
        {
            input.UI.Disable();

            input.UI.Continue.performed += SelectionButton;
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

        private void SelectionButton(InputAction.CallbackContext context)
        {            
            NextAction();
        }

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
            Close();
        }

        #endregion
    }
}