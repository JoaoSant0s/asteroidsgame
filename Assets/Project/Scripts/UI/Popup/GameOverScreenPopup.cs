using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;

namespace AsteroidsGame.UI.Popup
{
    public class GameOverScreenPopup : BasePopup
    {
        public delegate void OnRestartGame();
        public static OnRestartGame RestartGame; 

        [SerializeField]
        private Button restartButton;

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

#endregion

        private void SetButtonEvents()
        {            
            restartButton.onClick.AddListener(()=>
            {
                RestartGame?.Invoke();
                flagService.Raise(enableGameplayFlag);
                Hide();
            });
        }
    }
}