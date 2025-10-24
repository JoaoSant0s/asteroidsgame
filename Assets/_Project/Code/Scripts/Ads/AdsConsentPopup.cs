using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popups;
using JoaoSant0s.ServicePackage.General;

using AsteroidsGame.Save;

namespace AsteroidsGame.UI.Popup
{
    public class AdsConsentPopup : PopupBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Button agreeButton;
        [SerializeField]
        private Button disagreeButton;

        private PlayerPersistenceService playerPersistence;

        private void Start()
        {
            playerPersistence = Services.Get<PlayerPersistenceService>();

            agreeButton.onClick.AddListener(() =>
            {
                playerPersistence.SetSettingsAdConsentSelected(true);

                Close();
            });

            disagreeButton.onClick.AddListener(() =>
            {
                playerPersistence.SetSettingsAdConsentSelected(false);
                Close();
            });

        }
    }
}