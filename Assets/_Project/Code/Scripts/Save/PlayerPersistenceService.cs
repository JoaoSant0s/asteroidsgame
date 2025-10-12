using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;

using AsteroidsGame.Data;
using JoaoSant0s.ServicePackage.Save;
using JetBrains.Annotations;
using System.ComponentModel;

namespace AsteroidsGame.Save
{
    public class PlayerPersistenceService : Service
    {
        [SerializeField]
        private SaveConfig playerPersistenceConfig;

        private SaveLocalService saveService;

        private SettingsData localSettingsSave;
        private PlayerInfoData localPlayerSave;
        private LevelSaveData localLevelSave;

        #region Public Override Methods

        public override void OnInit()
        {
            this.playerPersistenceConfig = Resources.Load<SaveConfig>("GameConfigs/PlayerPersistenceConfig");
            this.saveService = Services.Get<SaveLocalService>();

            BuildLocalSettingsSave();
            BuildLocalPlayerSave();
            BuildLocalLevelSave();
        }

        #endregion

        #region Public Methods

        public bool ContainsPlayerSave() => this.saveService.Contains(this.playerPersistenceConfig.playerSaveKey);

        public SettingsData GetSettingsSave() => this.localSettingsSave;

        public PlayerInfoData GetPlayerSave() => this.localPlayerSave;

        public LevelSaveData GetLevelSave() => this.localLevelSave;

        public void SetPlayerScore(int newScore)
        {
            this.localPlayerSave.score = newScore;
            SavePlayerInfo();
        }

        public void SetPlayerLife(int newLife)
        {
            this.localPlayerSave.life = newLife;
            SavePlayerInfo();
        }

        public void SetSettingsMobileControllerOn(bool isOn)
        {
            this.localSettingsSave.isMobileControllerOn = isOn;
            SaveSettings();
        }

        public void CreatePlayerSave()
        {
            this.localPlayerSave = new PlayerInfoData(this.playerPersistenceConfig.spaceshipSpawnerData.maxSpaceshipLife);
            SavePlayerInfo();
        }

        public void SetLevel(int currentLevelIndex, int globalLevelIndex)
        {
            this.localLevelSave = new LevelSaveData(currentLevelIndex, globalLevelIndex);
            SaveLevelSave();
        }

        public void SetGameplayLevel(LevelGameplaySave gameplayLevelInfo)
        {
            this.localLevelSave.gameplayInfo = gameplayLevelInfo;
            SaveLevelSave();
        }

        public void ReturningLevelBy(int levelToReturnAfterGameOver)
        {
            this.localLevelSave.ReturningLevelBy(levelToReturnAfterGameOver);
            SaveLevelSave();
        }

        #endregion

        #region Private Methods

        private void BuildLocalPlayerSave()
        {
            this.localPlayerSave = this.saveService.Get<PlayerInfoData>(this.playerPersistenceConfig.playerSaveKey) ?? new PlayerInfoData(this.playerPersistenceConfig.spaceshipSpawnerData.maxSpaceshipLife);
        }

        private void BuildLocalLevelSave()
        {
            this.localLevelSave = this.saveService.Get<LevelSaveData>(this.playerPersistenceConfig.levelSaveKey) ?? new LevelSaveData(0, 0);
        }

        private void BuildLocalSettingsSave()
        {
            this.localSettingsSave = this.saveService.Get<SettingsData>(this.playerPersistenceConfig.settingsSaveKey) ?? new SettingsData();
        }

        private void SavePlayerInfo()
        {
            this.saveService.Set(this.playerPersistenceConfig.playerSaveKey, this.localPlayerSave);
        }

        private void SaveLevelSave()
        {
            this.saveService.Set(this.playerPersistenceConfig.levelSaveKey, this.localLevelSave);
        }

        private void SaveSettings()
        {
            this.saveService.Set(this.playerPersistenceConfig.settingsSaveKey, this.localSettingsSave);
        }

        #endregion
    }

    [Serializable]
    public class SettingsData
    {
        public bool isMobileControllerOn;

        public SettingsData()
        {
            isMobileControllerOn = true;
        }
    }

    [Serializable]
    public class PlayerInfoData
    {
        public int score;
        public int life;

        public PlayerInfoData(int maxLife)
        {
            life = maxLife;
            score = 0;
        }
    }

    [Serializable]
    public class LevelSaveData
    {
        public int levelIndex;
        public int globalLevelIndex;
        public LevelGameplaySave gameplayInfo;

        public LevelSaveData(int currentLevelIndex, int currentGlobalLevelIndex)
        {
            levelIndex = currentLevelIndex;
            globalLevelIndex = currentGlobalLevelIndex;
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
