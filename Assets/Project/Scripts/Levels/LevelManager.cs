using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.CustomVariable;

using AsteroidsGame.Spaceships.Data;
using AsteroidsGame.UI.Popup;
using AsteroidsGame.Spaceships;
using AsteroidsGame.Save;
using AsteroidsGame.Asteroids;
using AsteroidsGame.UtilWrapper.Data;
using AsteroidsGame.Levels.Data;

namespace AsteroidsGame.Levels
{
    public class LevelManager : MonoBehaviour
    {
        public static event Action OnMakeSpaceshipInvulnerable;
        public static event Action OnSavePlayerScore;
        public static event Action OnSavePlayerLife;
        public static event Action OnLevelSpawned;
        public static event Action<TupleKeyData> OnSpawnAsteroid;

        [Header("Configs")]
        [SerializeField]
        private LevelCollectionData data;

        [SerializeField]
        private SpaceshipSpawnerData spaceshipData;

        [Header("Variables")]
        [SerializeField]
        private IntVariable globalLevelVariable;

        private int currentLevelIndex = 0;
        private int globalLevelIndex = 0;

        private PopupService popupService;
        private PlayerPersistenceService playerPersistence;

        #region Unity Methods

        private void Awake()
        {
            AsteroidSpawner.SpawnNextLevel += GoNextLevel;
            SpaceshipSpawner.OnGameOver += OnGameOver;
        }

        private void Start()
        {
            popupService = Services.Get<PopupService>();
            playerPersistence = Services.Get<PlayerPersistenceService>();
        }

        private void OnDestroy()
        {
            AsteroidSpawner.SpawnNextLevel -= GoNextLevel;
            SpaceshipSpawner.OnGameOver -= OnGameOver;
        }

        #endregion

        #region Public Methods

        public void StartCurrentLevel(LevelSaveData levelSave)
        {
            OnMakeSpaceshipInvulnerable?.Invoke();

            SetPlayerLevelIndex(levelSave);

            this.globalLevelVariable.Modify(globalLevelIndex);

            if (levelSave.ContainsGameplayInfo())
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
            playerPersistence.SetPlayerLife(spaceshipData.maxSpaceshipLife);
            playerPersistence.ReturningLevelBy(data.levelToReturnAfterGameOver);

            var popup = popupService.Show<GameOverScreenPopup>();
            popup.UpdateMessage(data.levelToReturnAfterGameOver);
        }

        private void SetPlayerLevelIndex(LevelSaveData levelSave = null)
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

            playerPersistence.SetLevel(currentLevelIndex, globalLevelIndex);
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

            playerPersistence.SetGameplayLevel(info);
            OnLevelSpawned?.Invoke();
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

            playerPersistence.SetGameplayLevel(info);
            OnLevelSpawned?.Invoke();
        }

        private void SpawnAsteroidsAmount(TupleKeyData type, int amount)
        {
            for (int j = 0; j < amount; j++)
            {
                OnSpawnAsteroid?.Invoke(type);
            }
        }

        private IEnumerator GoNextLevelRoutine()
        {
            yield return new WaitForSeconds(data.nextLevelDelay);

            var popup = popupService.Show<NextLevelPopup>();
            popup.SetVisual(globalLevelIndex + 1);
            popup.SetGoAction(CreateCurrentLevel);
        }

        private void CreateCurrentLevel()
        {
            OnMakeSpaceshipInvulnerable?.Invoke();

            globalLevelVariable.Modify(globalLevelIndex);
            SpawnLevelContent();
        }

        #endregion
    }
}