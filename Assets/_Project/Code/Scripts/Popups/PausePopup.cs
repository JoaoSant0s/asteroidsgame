using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Popups;
using UnityEngine.UI;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;
using UnityEngine.InputSystem;
using JoaoSant0s.CustomVariable;
using AsteroidsGame.Save;
using UnityEditor.SceneManagement;

namespace AsteroidsGame.UI.Popup
{
    public class PausePopup : PopupBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Button resumeButton;
        [SerializeField]
        private Toggle mobileControllersToggle;

        [Header("Assets")]

        [SerializeField]
        private FlagAsset enableGameplayFlag;

        [SerializeField]
        private FlagAsset enableMobileControllerFlag;

        private FlagService flagService;
        private PlayerPersistenceService playerPersistence;
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
            input.UI.TempSelectToggle.performed += ToggleCheckbox;
            mobileControllersToggle.onValueChanged.AddListener(RefreshToggleCheckboxVisual);
        }
        private void Start()
        {
            flagService = Services.Get<FlagService>();
            playerPersistence = Services.Get<PlayerPersistenceService>();

            Setup();
            SetButtonEvents();
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            input.UI.Disable();

            input.UI.Continue.performed += SelectionButton;
            input.UI.TempSelectToggle.performed += ToggleCheckbox;
            mobileControllersToggle.onValueChanged.RemoveListener(RefreshToggleCheckboxVisual);
        }

        #endregion

        #region  Private Methods

        private void Setup()
        {
            flagService.Lower(enableGameplayFlag);

            mobileControllersToggle.isOn = playerPersistence.GetSettingsSave().isMobileControllerOn;
        }

        private void SelectionButton(InputAction.CallbackContext context)
        {
            NextAction();
        }

        private void ToggleCheckbox(InputAction.CallbackContext context)
        {
            mobileControllersToggle.isOn = !mobileControllersToggle.isOn;
        }

        private void RefreshToggleCheckboxVisual(bool isOn)
        {
            if (isOn)
            {
                flagService.Raise(enableMobileControllerFlag);
            }
            else
            {
                flagService.Lower(enableMobileControllerFlag);
            }
            playerPersistence.SetSettingsMobileControllerOn(isOn);
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