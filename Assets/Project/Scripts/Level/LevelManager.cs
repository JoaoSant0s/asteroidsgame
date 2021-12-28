using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Data;
using JoaoSant0s.CommonWrapper;
using AsteroidsGame.UI;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;
using AsteroidsGame.UI.Popup;

namespace AsteroidsGame.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public delegate void MakeSpaceshipInvulnerable();
        public static MakeSpaceshipInvulnerable OnMakeSpaceshipInvulnerable;

        [Header("Components")]
        [SerializeField]
        private LevelView levelView;

        [Header("Configs")]
        [SerializeField]
        private LevelCollectionData data;

        private int currentLevelIndex = 0;
        private int visualLevel = 0;

        private PopupService popupService;


        #region Unity Methods

        private void Awake()
        {
            AsteroidSpawner.SpawnNextLevel += GoNextLevel;
            visualLevel = currentLevelIndex;
        }

        private void Start()
        {
            popupService = Services.Get<PopupService>();
        }

        private void OnDestroy()
        {
            AsteroidSpawner.SpawnNextLevel -= GoNextLevel;
        }

        #endregion

        #region Public Methods

        public void Reset()
        {
            AsteroidSpawner.Instance.Reset();
            currentLevelIndex = 0;
            visualLevel = currentLevelIndex;
        }

        public void StartCurrentLevel()
        {
            OnMakeSpaceshipInvulnerable?.Invoke();
            levelView.UpdateLevel(visualLevel + 1);
            SpawnLevelContent();
        }

        #endregion

        #region Private Methods

        private void GoNextLevel()
        {
            currentLevelIndex++;
            visualLevel++;

            if (currentLevelIndex >= data.levels.Count) currentLevelIndex = 0;

            StartCoroutine(GoNextLevelRoutine());
        }

        private void SpawnLevelContent()
        {
            var level = data.levels[currentLevelIndex];

            for (int i = 0; i < level.Configs.Count; i++)
            {
                var config = level.Configs[i];

                for (int j = 0; j < config.RandomAmount; j++)
                {
                    AsteroidSpawner.Instance.SpawnAsteroid(config.asteroidType);
                }
            }
        }

        private IEnumerator GoNextLevelRoutine()
        {
            yield return new WaitForSeconds(data.nextLevelDelay);

            var popup = popupService.Show<NextLevelPopup>();
            popup.SetVisual(visualLevel + 1);
            popup.SetGoAction(StartCurrentLevel);
        }

        #endregion
    }
}