using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.CommonWrapper;

using AsteroidsGame.Unit;
using AsteroidsGame.Data;
using AsteroidsGame.Actions;
using AsteroidsGame.UI;

namespace AsteroidsGame.Manager
{
    public class SpaceshipSpawner : SingletonBehaviour<SpaceshipSpawner>
    {
        public delegate void OnUpdateSpaceshipLife(int value);
        public static OnUpdateSpaceshipLife UpdateSpaceshipLife;

        public delegate void PlayerGameOver();
        public static PlayerGameOver OnGameOver;

        public delegate void EnabeRewardButton(bool enable, UnityAction action = null);
        public static EnabeRewardButton OnEnabeRewardButton;

        [SerializeField]
        private Spaceship spaceshipPrefab;

        [SerializeField]
        private SpaceshipSpawnerData data;

        [SerializeField]
        private Transform pulletsArea;

        private int spaceshipLife;

        private PopupService popupService;

        private Spaceship currentSpaceship;

        private bool extraLifeUsed;

        public Transform BulletsArea => pulletsArea;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            SpaceshipCollisionListener.AsteroidCollided += SpaceshipDestroyed;
            RewardedVideoButton.ShowRewardedVideo += RewardedVideoStarted;
            LevelManager.OnMakeSpaceshipInvulnerable += MakeSpaceshipInvulnerable;
            LevelManager.OnSavePlayerLife += SaveLife;
        }

        private void Start()
        {
            popupService = Services.Get<PopupService>();
        }

        private void OnDestroy()
        {
            SpaceshipCollisionListener.AsteroidCollided -= SpaceshipDestroyed;
            RewardedVideoButton.ShowRewardedVideo -= RewardedVideoStarted;
            LevelManager.OnMakeSpaceshipInvulnerable -= MakeSpaceshipInvulnerable;
            LevelManager.OnSavePlayerLife -= SaveLife;
        }

        #endregion

        #region Public Methods

        public void SetLife(int newLife)
        {
            spaceshipLife = newLife;
            UpdateSpaceshipLife?.Invoke(spaceshipLife);
        }

        public void SpawnSpaceship(bool makeInvulnarable = false)
        {
            currentSpaceship = Instantiate(spaceshipPrefab, Vector3.zero, Quaternion.identity);
            if (makeInvulnarable) MakeSpaceshipInvulnerable();
        }

        #endregion

        #region Private Methods    

        private void MakeSpaceshipInvulnerable()
        {
            if (currentSpaceship == null) return;
            currentSpaceship.InvulnerableAction?.RunDefaultInvulnerability();
        }

        private void RewardedVideoStarted(UnityAction action)
        {
            if (currentSpaceship == null) return;
            currentSpaceship.InvulnerableAction?.RunInfinityInvulnerability();
        }

        private void ModifyLife(int increment)
        {
            spaceshipLife += increment;
            UpdateSpaceshipLife?.Invoke(spaceshipLife);
        }

        private void SpaceshipDestroyed()
        {
            OnEnabeRewardButton?.Invoke(false);

            ModifyLife(-1);
            if (spaceshipLife <= 0)
            {
                OnGameOver?.Invoke();
                extraLifeUsed = false;
                return;
            }

            StartCoroutine(RespawnSpaceshipRoutine());
        }

        private IEnumerator RespawnSpaceshipRoutine()
        {
            yield return new WaitForSeconds(data.respawnDelay);
            SpawnSpaceship(true);
            CheckRewardLife();
        }

        private void SaveLife()
        {
            SaveManager.Instance.SetPlayerLife(spaceshipLife);
        }

        private void CheckRewardLife()
        {
            if (spaceshipLife > data.minRewardAdsLifeLimit || extraLifeUsed) return;

            OnEnabeRewardButton?.Invoke(true, AddExtraLife);
        }

        private void AddExtraLife()
        {
            if (currentSpaceship == null) return;
            extraLifeUsed = true;

            currentSpaceship.InvulnerableAction?.StopInvulnerability();
            ModifyLife(data.rewardAdsLifeGain);
        }

        #endregion
    }
}