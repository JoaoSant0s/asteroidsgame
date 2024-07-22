using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;

using AsteroidsGame.UI.Popup;
using UnityEngine.InputSystem;

namespace AsteroidsGame.UI.Inputs
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        private Button buttonPause;
        private PopupService popupService;
        private InputControls input;

        #region Unity Methods

        private void Awake()
        {
            buttonPause = GetComponent<Button>();
            popupService = Services.Get<PopupService>();
            input = new InputControls();
        }
        private void OnEnable()
        {
            input.UI.Enable();

            input.UI.Pause.performed += SelectionButton;
        }

        private void Start()
        {
            buttonPause.onClick.AddListener(() =>
            {
                NextAction();
            });
        }

        private void OnDisable()
        {
            input.UI.Disable();

            input.UI.Pause.performed += SelectionButton;
        }

        private void SelectionButton(InputAction.CallbackContext context)
        {
            NextAction();
        }

        private void NextAction()
        {
            popupService.Show<PausePopup>();
        }

        #endregion
    }
}
