using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Main.ServicePackage.Popup;
using Main.ServicePackage.Flag;
using Main.ServicePackage.General;

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