using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.Flag;
using JoaoSant0s.ServicePackage.General;

namespace AsteroidsGame.UI.Popup
{
    public class SplashScreenPopup : BasePopup
    {
        [SerializeField]
        private Button startButton;

        [SerializeField]
        private FlagAsset enableGameplayFlag;

        private FlagService flagService;

#region Unity Methods
        private void Start() 
        {
            flagService = Services.Get<FlagService>();
            SetButtonEvents();  
        }

#endregion

        private void SetButtonEvents()
        {            
            startButton.onClick.AddListener(()=>
            {
                flagService.Raise(enableGameplayFlag);
                Hide();
            });
        }
    }
}