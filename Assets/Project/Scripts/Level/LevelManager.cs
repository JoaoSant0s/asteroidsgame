using System;
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
        public static event Action OnMakeSpaceshipInvulnerable;
        public static event Action OnSavePlayerScore;
        public static event Action OnSavePlayerLife;
        public static event Action<int> OnLevelStarted;

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

            OnLevelStarted?.Invoke(globalLevelIndex + 1);

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

                var wasSaved = info.asteroidsAmount.Count < i;

                var amount = wasSaved ? info.asteroidsAmount[i] : config.RandomAmount;

                if (!wasSaved)
                {
                    info.asteroidsAmount.Add(amount);
                }

                SpawnAsteroidsAmount(config.asteroidType, amount);
            }

            SaveManager.Instance.SetPlayerGameplayLevel(info);
            AsteroidSpawner.Instance.UpdateAsteroidsCounter();
        }

        private void SpawnLevelContent()
        {
            var level = data.levels[currentLevelIndex];
            var info = new LevelGameplaySave();

            for (int i = 0; i < level.Configs.Count; i++)
            {
                var config = level.Configs[i];
                var amount = config.RandomAmount;

                info.asteroidsAmount.Add(amount);
                SpawnAsteroidsAmount(config.asteroidType, amount);
            }

            SaveManager.Instance.SetPlayerGameplayLevel(info);
            AsteroidSpawner.Instance.UpdateAsteroidsCounter();
        }

        private void SpawnAsteroidsAmount(TupleKeyData type, int amount)
        {
            for (int j = 0; j < amount; j++)
            {
                AsteroidSpawner.Instance.SpawnAsteroid(type);
            }
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