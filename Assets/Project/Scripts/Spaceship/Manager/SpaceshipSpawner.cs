using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.CustomVariable;
using JoaoSant0s.CommonWrapper;

using AsteroidsGame.Unit;
using AsteroidsGame.Data;
using AsteroidsGame.Actions;
using AsteroidsGame.UI;
using AsteroidsGame.Level;

namespace AsteroidsGame.Manager
{
    public class SpaceshipSpawner : SingletonBehaviour<SpaceshipSpawner>
    {
        public static event Action OnGameOver;
        public static event Action<bool, UnityAction> OnEnabeRewardButton;

        [SerializeField]
        private Spaceship spaceshipPrefab;

        [SerializeField]
        private SpaceshipSpawnerData data;

        [SerializeField]
        private Transform pulletsArea;

        [Header("Variables")]
        [SerializeField]
        private IntVariable lifeVariable;

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
            this.lifeVariable.Modify(newLife);
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

        private void RewardedVideoStarted()
        {
            if (currentSpaceship == null) return;
            currentSpaceship.InvulnerableAction?.RunInfinityInvulnerability();
        }

        private void SaveLife()
        {
            SaveManager.Instance.SetPlayerLife(this.lifeVariable.Value);
        }

        private void SpaceshipDestroyed()
        {
            OnEnabeRewardButton?.Invoke(false, () => { });

            ModifyLife(-1);
            if (this.lifeVariable.Value <= 0)
            {
                OnGameOver?.Invoke();
                extraLifeUsed = false;
                return;
            }

            StartCoroutine(RespawnSpaceshipRoutine());
        }

        private void ModifyLife(int value)
        {
            this.lifeVariable.Add(value);
        }

        private IEnumerator RespawnSpaceshipRoutine()
        {
            yield return new WaitForSeconds(data.respawnDelay);
            SpawnSpaceship(true);
            CheckRewardLife();
        }

        private void CheckRewardLife()
        {
            if (this.lifeVariable.Value > data.minRewardAdsLifeLimit || extraLifeUsed) return;

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