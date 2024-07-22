using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.CustomVariable;

using AsteroidsGame.Spaceships.Data;
using AsteroidsGame.Spaceships.Actions;
using AsteroidsGame.Levels;
using AsteroidsGame.Save;
using AsteroidsGame.Ads.UI.Inputs;
using AsteroidsGame.Ads;
using JoaoSant0s.CommonWrapper;

namespace AsteroidsGame.Spaceships
{
    public class SpaceshipSpawner : MonoBehaviour
    {
        public static event Action OnGameOver;
        public static event Action<bool, Action<AdsResult>> OnEnabeRewardButton;

        [Header("References")]

        [SerializeField]
        private Transform bulletsArea;

        [SerializeField]
        private Transform spaceshipArea;

        [Header("Data")]
        [SerializeField]
        private Spaceship spaceshipPrefab;

        [SerializeField]
        private SpaceshipSpawnerData spaceshipSpawnerData;

        [Header("Variables")]
        [SerializeField]
        private IntVariable lifeVariable;

        private PlayerPersistenceService playerPersistence;

        private static Spaceship currentSpaceship;
        private bool extraLifeUsed;

        #region Unity Methods

        private void Awake()
        {
            SpaceshipCollisionListener.SpaceshipCollided += SpaceshipDestroyed;
            RewardedVideoButton.ShowRewardedVideo += RewardedVideoStarted;
            LevelManager.OnMakeSpaceshipInvulnerable += MakeSpaceshipInvulnerable;
            LevelManager.OnSavePlayerLife += SaveLife;
            SpaceshipShootAction.GetBulletArea += GetBulletArea;
        }

        private void Start()
        {
            playerPersistence = Services.Get<PlayerPersistenceService>();
        }

        private void OnDestroy()
        {
            SpaceshipCollisionListener.SpaceshipCollided -= SpaceshipDestroyed;
            RewardedVideoButton.ShowRewardedVideo -= RewardedVideoStarted;
            LevelManager.OnMakeSpaceshipInvulnerable -= MakeSpaceshipInvulnerable;
            LevelManager.OnSavePlayerLife -= SaveLife;
            SpaceshipShootAction.GetBulletArea -= GetBulletArea;
        }

        #endregion

        #region Public Methods

        public void SetLife(int newLife)
        {
            this.lifeVariable.Value = newLife;
        }

        public void SpawnSpaceship()
        {
            currentSpaceship = Instantiate(spaceshipPrefab, Vector3.zero, Quaternion.identity, spaceshipArea);
            MakeSpaceshipInvulnerable();
        }

        public static async Task<Spaceship> WaitCurrentSpaceship()
        {
            if (currentSpaceship) return currentSpaceship;

            while (currentSpaceship == null)
            {
                await Task.Delay(100);
            }

            return currentSpaceship;
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
            playerPersistence.SetPlayerLife(this.lifeVariable.Value);
        }

        private void SpaceshipDestroyed()
        {
            currentSpaceship = null;
            OnEnabeRewardButton?.Invoke(false, (result) => { });

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
            this.lifeVariable.Increment(value);
        }

        private IEnumerator RespawnSpaceshipRoutine()
        {
            yield return new WaitForSeconds(spaceshipSpawnerData.respawnDelay);
            SpawnSpaceship();
            CheckRewardLife();
        }

        private void CheckRewardLife()
        {
            if (this.lifeVariable.Value > spaceshipSpawnerData.minRewardAdsLifeLimit || extraLifeUsed) return;

            OnEnabeRewardButton?.Invoke(true, AddExtraLife);
        }

        private void AddExtraLife(AdsResult result)
        {
            if (result == AdsResult.Failed || currentSpaceship == null) return;
            extraLifeUsed = true;

            currentSpaceship.InvulnerableAction?.StopInvulnerability();
            ModifyLife(spaceshipSpawnerData.rewardAdsLifeGain);
        }

        private Transform GetBulletArea()
        {
            return bulletsArea;
        }

        #endregion
    }
}