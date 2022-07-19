using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;

using AsteroidsGame.UI.Popup;
using AsteroidsGame.Level;
using AsteroidsGame.Save;
using AsteroidsGame.Asteroid;

namespace AsteroidsGame.Manager
{
    public class GameScene : MonoBehaviour
    {
        [Header("Components")]

        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private ScoreManager scoreManager;

        [SerializeField]
        private SpaceshipSpawner spaceshipSpawner;

        [SerializeField]
        private AsteroidSpawner asteroidSpawner;

        private PopupService popupService;

        private PlayerPersistenceService playerPersistence;


        #region Unity Methods
        private void Start()
        {
            popupService = Services.Get<PopupService>();
            playerPersistence = Services.Get<PlayerPersistenceService>();

            GameOverScreenPopup.RestartGame += RestartGame;

            StartCoroutine(ShowSplashScreenRoutine());
        }

        private void OnDestroy()
        {
            GameOverScreenPopup.RestartGame -= RestartGame;
        }

        #endregion

        #region Private Methods

        private IEnumerator ShowSplashScreenRoutine()
        {
            yield return new WaitForEndOfFrame();

            var popup = popupService.Show<SplashScreenPopup>();

            popup.OnBeforeClose += StartGame;
        }

        private void RestartGame()
        {
            asteroidSpawner.Reset();

            StartGame();
        }

        private void StartGame()
        {
            var playerSave = playerPersistence.GetPlayerSave();

            spaceshipSpawner.SetLife(playerSave.life);
            scoreManager.SetScore(playerSave.score);

            spaceshipSpawner.SpawnSpaceship();
            levelManager.StartCurrentLevel(playerPersistence.GetLevelSave());
        }

        #endregion
    }
}