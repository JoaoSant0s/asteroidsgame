using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;

using AsteroidsGame.Data;
using AsteroidsGame.UI;
using AsteroidsGame.UI.Popup;

namespace AsteroidsGame.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public delegate void MakeSpaceshipInvulnerable();
        public static MakeSpaceshipInvulnerable OnMakeSpaceshipInvulnerable;

        public delegate void SavePlayerProperties();
        public static SavePlayerProperties OnSavePlayerScore;
        public static SavePlayerProperties OnSavePlayerLife;

        [Header("Components")]
        [SerializeField]
        private LevelView levelView;

        [Header("Configs")]
        [SerializeField]
        private LevelCollectionData data;

        [SerializeField]
        private SpaceshipSpawnerData spaceshipData;

        private int currentLevelIndex = 0;
        private int globalLevelIndex = 0;

        private PopupService popupService;


        #region Unity Methods

        private void Awake()
        {
            AsteroidSpawner.SpawnNextLevel += GoNextLevel;
            SpaceshipSpawner.OnGameOver += OnGameOver;
        }

        private void Start()
        {
            popupService = Services.Get<PopupService>();
        }

        private void OnDestroy()
        {
            AsteroidSpawner.SpawnNextLevel -= GoNextLevel;
            SpaceshipSpawner.OnGameOver -= OnGameOver;
        }

        #endregion

        #region Public Methods

        public void StartCurrentLevel(LevelSave levelSave = null)
        {
            OnMakeSpaceshipInvulnerable?.Invoke();

            SetPlayerLevelIndex(levelSave);

            levelView.UpdateLevel(globalLevelIndex + 1);

            if (levelSave != null && levelSave.ContainsGameplayInfo())
            {
                SpawnLevelContent(levelSave.gameplayInfo);
            }
            else
            {
                SpawnLevelContent();
            }
        }

        #endregion

        #region Private Methods

        private void OnGameOver()
        {
            var save = SaveManager.Instance.GetPlayerSave();

            save.life = spaceshipData.maxSpaceshipLife;
            save.level.ReturningLevelBy(data.levelToReturnAfterGameOver);

            SaveManager.Instance.SetPlayer(save);

            var popup = popupService.Show<GameOverScreenPopup>();
            popup.UpdateMessage(data.levelToReturnAfterGameOver);
        }

        private void SetPlayerLevelIndex(LevelSave levelSave = null)
        {
            if (levelSave == null) return;

            globalLevelIndex = levelSave.globalLevelIndex;
            currentLevelIndex = levelSave.levelIndex;
        }

        private void GoNextLevel()
        {
            currentLevelIndex++;
            globalLevelIndex++;

            if (currentLevelIndex >= data.levels.Count) currentLevelIndex = 0;

            SaveManager.Instance.SetPlayerLevel(new LevelSave(currentLevelIndex, globalLevelIndex));
            OnSavePlayerScore?.Invoke();
            OnSavePlayerLife?.Invoke();

            StartCoroutine(GoNextLevelRoutine());
        }

        private void SpawnLevelContent(LevelGameplaySave info)
        {
            var level = data.levels[currentLevelIndex];

            for (int i = 0; i < level.Configs.Count; i++)
            {
                var config = level.Configs[i];
                var amount = info.asteroidsAmount[i];

                for (int j = 0; j < amount; j++)
                {
                    AsteroidSpawner.Instance.SpawnAsteroid(config.asteroidType);
                }
            }
        }

        private void SpawnLevelContent()
        {
            var level = data.levels[currentLevelIndex];
            var gameplayLevelInfo = new LevelGameplaySave();

            for (int i = 0; i < level.Configs.Count; i++)
            {
                var config = level.Configs[i];
                var amount = config.RandomAmount;

                gameplayLevelInfo.asteroidsAmount.Add(amount);

                for (int j = 0; j < amount; j++)
                {
                    AsteroidSpawner.Instance.SpawnAsteroid(config.asteroidType);
                }
            }

            SaveManager.Instance.SetPlayerGameplayLevel(gameplayLevelInfo);
        }

        private IEnumerator GoNextLevelRoutine()
        {
            yield return new WaitForSeconds(data.nextLevelDelay);

            var popup = popupService.Show<NextLevelPopup>();
            popup.SetVisual(globalLevelIndex + 1);
            popup.SetGoAction(() => StartCurrentLevel());
        }

        #endregion
    }
}