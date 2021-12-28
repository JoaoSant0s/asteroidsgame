using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Unit;
using AsteroidsGame.Data;
using AsteroidsGame.Actions;
using AsteroidsGame.UI.Popup;

using JoaoSant0s.ServicePackage.Popup;
using JoaoSant0s.ServicePackage.General;

namespace AsteroidsGame.Manager
{
    public class SpaceshipSpawner : MonoBehaviour
    {
        public delegate void OnUpdateSpaceshipLife(int value);
        public static OnUpdateSpaceshipLife UpdateSpaceshipLife;

        [SerializeField]
        private Spaceship spaceshipPrefab;

        [SerializeField]
        private SpaceshipSpawnerData data;

        private int spaceshipLife;

        private PopupService popupService;

        private Spaceship currentSpaceship;

        #region Unity Methods

        private void Awake()
        {
            SpaceshipCollisionListener.AsteroidCollided += SpaceshipDestroyed;
            LevelManager.OnMakeSpaceshipInvulnerable += MakeSpaceshipInvulnerable;
        }

        private void Start()
        {
            popupService = Services.Get<PopupService>();
        }

        private void OnDestroy()
        {
            SpaceshipCollisionListener.AsteroidCollided -= SpaceshipDestroyed;
            LevelManager.OnMakeSpaceshipInvulnerable -= MakeSpaceshipInvulnerable;
        }

        #endregion

        public void Reset()
        {
            spaceshipLife = data.maxSpaceshipLife;
            UpdateSpaceshipLife?.Invoke(spaceshipLife);
        }

        public void SpawnSpaceship()
        {
            Reset();
            RespawnSpaceship();
        }

        public void RespawnSpaceship()
        {
            currentSpaceship = Instantiate(spaceshipPrefab, Vector3.zero, Quaternion.identity);
            MakeSpaceshipInvulnerable();
        }

        private void MakeSpaceshipInvulnerable()
        {
            if (currentSpaceship == null) return;
            currentSpaceship.RunDefaultInvulnerability();
        }

        private IEnumerator RespawnSpaceshipRoutine()
        {
            yield return new WaitForSeconds(data.respawnDelay);
            RespawnSpaceship();
        }

        private void SpaceshipDestroyed()
        {
            spaceshipLife--;

            UpdateSpaceshipLife?.Invoke(spaceshipLife);
            if (spaceshipLife <= 0)
            {
                popupService.Show<GameOverScreenPopup>();
                return;
            }

            StartCoroutine(RespawnSpaceshipRoutine());
        }
    }
}