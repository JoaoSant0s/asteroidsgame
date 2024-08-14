using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using AsteroidsGame.Data;

using JoaoSant0s.ServicePackage.Popups;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Flag;

using TMPro;

namespace AsteroidsGame.UI.Popup
{
    public class NextLevelPopup : PopupBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Button continueButton;
        [SerializeField]
        private TextMeshProUGUI levelLabel;

        [Header("Data")]
        [SerializeField]
        private FlagAsset enableGameplayFlag;
        private FlagService flagService;

        private UnityAction action;
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

        public void SetVisual(int visualLevel)
        {
            levelLabel.text = string.Format("Wave {0}", visualLevel);
        }
        public void SetGoAction(UnityAction action)
        {
            this.action = action;
        }

        #endregion

        #region  Private Methods

        private void SelectionButton(InputAction.CallbackContext context)
        {
            NextAction();
        }
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
            Close();
        }

        #endregion
    }
}
