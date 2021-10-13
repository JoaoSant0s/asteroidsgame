using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Main.Scene;
using AsteroidsGame.Unit;
using AsteroidsGame.UI.Popup;
using Main.ServicePackage.General;
using Main.ServicePackage.Popup;

namespace AsteroidsGame.Manager
{
    public class GameScene : MainScene
    {

        [SerializeField]
        private SpaceshipSpawner spaceshipSpawner;

        [SerializeField]
        private LevelManager levelManager;        

        private PopupService popupService;

#region Unity Methods
        private void Start() 
        {
            popupService = Services.Get<PopupService>();            

            var popup = popupService.ShowPopup<SplashScreenPopup>();

            popup.OnBeforeHide += StartGame;

            GameOverScreenPopup.RestartGame += RestartGame;
        }

        private void OnDestroy() 
        {
            GameOverScreenPopup.RestartGame -= RestartGame;
        }

#endregion

        private void RestartGame()
        {
            spaceshipSpawner.Reset();
            levelManager.Reset();

            StartGame();
        }

        private void StartGame()
        {
            spaceshipSpawner.SpawnSpaceship();
            levelManager.SpawnLevelContent();
        }
    }
}