using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Unit;
using AsteroidsGame.UI.Popup;

using JoaoSant0s.Scene;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;

namespace AsteroidsGame.Manager
{
    public class GameScene : MonoBehaviour
    {

        [SerializeField]
        private SpaceshipSpawner spaceshipSpawner;

        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private ScoreManager scoreManager;  

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
            scoreManager.Reset();

            StartGame();
        }

        private void StartGame()
        {
            spaceshipSpawner.SpawnSpaceship();
            levelManager.SpawnLevelContent();
        }
    }
}