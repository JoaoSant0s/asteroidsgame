using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popup;
using TMPro;
using JoaoSant0s.ServicePackage.Console;

namespace AsteroidsGame.UI.Console
{
    public class ConsolePopup : BasePopup
    {
        [Header("Components")]
        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private Button clearButton;

        [SerializeField]
        private TextMeshProUGUI logLabel;

        private ConsoleManager console;

        #region Unity Methods

        private void Awake()
        {
            console = ConsoleManager.Instance;
        }

        private void Start()
        {
            console.OnLogAdded += OLogAdded;
            closeButton.onClick.AddListener(Close);
            clearButton.onClick.AddListener(() => { logLabel.text = ""; });
        }

        #endregion

        #region Protected Override Methods

        protected override void BeforeClose()
        {
            console.OnLogAdded -= OLogAdded;
        }

        #endregion

        #region Private Methods

        private void OLogAdded(LogObject log)
        {
            logLabel.text += $"{log.type.ToString()} - {log.logString} \n\n";
        }

        #endregion
    }
}
