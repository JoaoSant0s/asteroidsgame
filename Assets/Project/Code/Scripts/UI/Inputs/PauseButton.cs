using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;

using AsteroidsGame.UI.Popup;

namespace AsteroidsGame.UI.Inputs
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        private Button buttonPause;
        private PopupService popupService;

        #region Unity Methods

        private void Awake()
        {
            buttonPause = GetComponent<Button>();
            popupService = Services.Get<PopupService>();
        }

        private void Start()
        {
            buttonPause.onClick.AddListener(() =>
            {
                popupService.Show<PausePopup>();
            });
        }

        #endregion
    }
}
