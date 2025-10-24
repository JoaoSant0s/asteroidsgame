using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popups;
using JoaoSant0s.ServicePackage.Flag;

using AsteroidsGame.UI.Popup;
using AsteroidsGame.Levels;
using AsteroidsGame.Save;
using AsteroidsGame.Asteroids;
using AsteroidsGame.Scores;
using AsteroidsGame.Spaceships;
using AsteroidsGame.Challenges;
using AsteroidsGame.UI;
using AsteroidsGame.Ads;

namespace AsteroidsGame.Manager
{
    public class GameScene : MonoBehaviour
    {
        [Header("Components")]

        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private ChallengeManager challengeManager;

        [SerializeField]
        private ScoreManager scoreManager;

        [SerializeField]
        private SpaceshipSpawner spaceshipSpawner;

        [SerializeField]
        private AsteroidSpawner asteroidSpawner;

        [Header("Assets")]

        [SerializeField]
        private FlagAsset enableMobileControllerFlag;

        private PopupService popupService;

        private PlayerPersistenceService playerPersistence;
        private FlagService flagService;
        private AdsService adsService;


        #region Unity Methods
        private void Start()
        {
            popupService = Services.Get<PopupService>();
            playerPersistence = Services.Get<PlayerPersistenceService>();
            flagService = Services.Get<FlagService>();
            adsService = Services.Get<AdsService>();

            GameOverScreenPopup.RestartGame += RestartGame;

            if (adsService.WasConsentSelected())
            {
                InitGame();
            }
            else
            {
                var popup = popupService.Show<AdsConsentPopup>();
                popup.OnBeforeClose += InitGame;
            }

        }

        private void OnDestroy()
        {
            GameOverScreenPopup.RestartGame -= RestartGame;
        }

        #endregion

        #region Private Methods

        private void InitGame()
        {
            adsService.StartUnityAds();
            StartCoroutine(ShowSplashScreenRoutine());
        }

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

            challengeManager.Init();
            spaceshipSpawner.SetLife(playerSave.life);
            scoreManager.SetScore(playerSave.score);

            levelManager.StartCurrentLevel(playerPersistence.GetLevelSave());
            spaceshipSpawner.SpawnSpaceship();

            ToggleMobileController(playerPersistence.GetSettingsSave().isMobileControllerOn);
        }

        private void ToggleMobileController(bool isOn)
        {
            if (isOn)
            {
                flagService.Raise(enableMobileControllerFlag);
            }
            else
            {
                flagService.Lower(enableMobileControllerFlag);
            }
        }

        #endregion
    }
}