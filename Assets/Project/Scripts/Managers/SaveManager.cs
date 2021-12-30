using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper;
using JoaoSant0s.ServicePackage.Save;
using JoaoSant0s.ServicePackage.General;
using AsteroidsGame.Data;

namespace AsteroidsGame.Manager
{
    public class SaveManager : SingletonBehaviour<SaveManager>
    {
        [SerializeField]
        private SpaceshipSpawnerData data;

        [SerializeField]
        private SaveConfig saveConfig;
        private SaveLocalService saveService;
        private PlayerSave localPlayerSave;

        #region Unity Methods

        private void Start()
        {
            saveService = Services.Get<SaveLocalService>();
        }

        #endregion

        #region Public Methods

        public bool ContainsPlayerSave()
        {
            return saveService.Contains(saveConfig.playerSaveKey);
        }

        public PlayerSave GetPlayerSave()
        {
            if (localPlayerSave == null) SetLocalPlayerSave();

            return localPlayerSave;
        }

        public void SetPlayerScore(int newScore)
        {
            localPlayerSave.score = newScore;
            saveService.Set<PlayerSave>(saveConfig.playerSaveKey, localPlayerSave);
        }

        public void SetPlayerLife(int newLife)
        {
            localPlayerSave.life = newLife;
            saveService.Set<PlayerSave>(saveConfig.playerSaveKey, localPlayerSave);
        }

        public void SetPlayerLevel(LevelSave newLevel)
        {
            localPlayerSave.level = newLevel;
            saveService.Set<PlayerSave>(saveConfig.playerSaveKey, localPlayerSave);
        }

        public void SetPlayerGameplayLevel(LevelGameplaySave gameplayLevelInfo)
        {
            localPlayerSave.level.gameplayInfo = gameplayLevelInfo;
            saveService.Set<PlayerSave>(saveConfig.playerSaveKey, localPlayerSave);
        }

        public void SetPlayer(PlayerSave player)
        {
            localPlayerSave = player;
            saveService.Set<PlayerSave>(saveConfig.playerSaveKey, localPlayerSave);
        }

        public void CreatePlayerSave()
        {
            localPlayerSave = new PlayerSave(data.maxSpaceshipLife);
            saveService.Set<PlayerSave>(saveConfig.playerSaveKey, localPlayerSave);
        }

        #endregion

        #region Private Methods

        private void SetLocalPlayerSave()
        {
            if (!saveService.Contains(saveConfig.playerSaveKey))
            {
                CreatePlayerSave();
            }
            else
            {
                localPlayerSave = saveService.Get<PlayerSave>(saveConfig.playerSaveKey);
            }
        }

        #endregion
    }

    [Serializable]
    public class PlayerSave
    {
        public LevelSave level;
        public int score;
        public int life;

        public PlayerSave(int maxLife)
        {
            life = maxLife;
            score = 0;
            level = new LevelSave(0, 0);
        }
    }

    [Serializable]
    public class LevelSave
    {
        public int levelIndex;
        public int globalLevelIndex;
        public LevelGameplaySave gameplayInfo;

        public LevelSave(int currentLevelIndex, int currentGlobalLevelIndex)
        {
            levelIndex = currentLevelIndex;
            globalLevelIndex = currentGlobalLevelIndex;
            gameplayInfo = null;
        }

        public bool ContainsGameplayInfo()
        {
            return gameplayInfo != null && gameplayInfo.asteroidsAmount.Count > 0;
        }

        public void ReturningLevelBy(int amount)
        {
            globalLevelIndex = Mathf.Max(0, globalLevelIndex - amount);
            levelIndex = Mathf.Max(0, levelIndex - amount);
            gameplayInfo = null;
        }
    }

    [Serializable]
    public class LevelGameplaySave
    {
        public List<int> asteroidsAmount;

        public LevelGameplaySave()
        {
            asteroidsAmount = new List<int>();
        }
    }


}