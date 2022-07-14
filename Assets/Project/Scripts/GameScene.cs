using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;

using AsteroidsGame.UI.Popup;

namespace AsteroidsGame.Manager
{
    public class GameScene : MonoBehaviour
    {
        [Header("Managers")]

        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private ScoreManager scoreManager;

        [Header("Others")]

        [SerializeField]
        private SpaceshipSpawner spaceshipSpawner;

        private PopupService popupService;

        #region Unity Methods
        private void Start()
        {
            popupService = Services.Get<PopupService>();
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
            AsteroidSpawner.Instance.Reset();

            StartGame();
        }

        private void StartGame()
        {
            var playerSave = SaveManager.Instance.GetPlayerSave();

            spaceshipSpawner.SetLife(playerSave.life);
            scoreManager.SetScore(playerSave.score);

            spaceshipSpawner.SpawnSpaceship();
            levelManager.StartCurrentLevel(playerSave.level);
        }

        #endregion
    }
}