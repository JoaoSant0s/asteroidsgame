using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;

using AsteroidsGame.UI.Console;

namespace AsteroidsGame.Console
{
    [RequireComponent(typeof(Button))]
    public class ConsoleButton : MonoBehaviour
    {
        private Button button;
        private PopupService popupService;

        #region Unity Methods

        private void Awake()
        {
            popupService = Services.Get<PopupService>(); ;
            this.button = GetComponent<Button>();
        }

        private void Start()
        {
            this.button.onClick.AddListener(() =>
            {
                popupService.Show<ConsolePopup>();
            });
        }

        #endregion
    }
}
