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
            GameOverScreenPopup.RestartGame += RestartGame;

            StartCoroutine(ShowSplashScreenRoutine());
        }

        private void OnDestroy()
        {
            GameOverScreenPopup.RestartGame -= RestartGame;
        }

        #endregion

        #region Private Methods

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

        private IEnumerator ShowSplashScreenRoutine()
        {
            yield return new WaitForEndOfFrame();

            var popup = popupService.Show<SplashScreenPopup>();

            popup.OnBeforeHide += StartGame;
        }

        #endregion
    }
}