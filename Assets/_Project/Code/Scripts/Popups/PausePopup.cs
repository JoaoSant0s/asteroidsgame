using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Popup;
using UnityEngine.UI;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;
using UnityEngine.InputSystem;

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
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            input.UI.Disable();

            input.UI.Continue.performed += SelectionButton;
        }

        #endregion

        #region  Private Methods

        private void SelectionButton(InputAction.CallbackContext context)
        {
            NextAction();
        }
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
            Time.timeScale = 1;
            Close();
        }

        #endregion
    }
}