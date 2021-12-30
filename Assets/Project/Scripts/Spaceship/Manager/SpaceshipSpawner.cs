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
            LevelManager.OnSavePlayerLife += SaveLife;
        }

        private void Start()
        {
            popupService = Services.Get<PopupService>();
        }

        private void OnDestroy()
        {
            SpaceshipCollisionListener.AsteroidCollided -= SpaceshipDestroyed;
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
            var action = currentSpaceship.GetComponent<SpaceshipInvulnerableAction>();
            action?.RunDefaultInvulnerability();
        }

        private void SpaceshipDestroyed()
        {
            spaceshipLife--;

            UpdateSpaceshipLife?.Invoke(spaceshipLife);
            if (spaceshipLife <= 0)
            {
                PlayerSaveGameOver();
                popupService.Show<GameOverScreenPopup>();
                
                return;
            }


            StartCoroutine(RespawnSpaceshipRoutine());
        }

        private void SaveLife()
        {
            SaveManager.Instance.SetPlayerLife(spaceshipLife);
        }

        private void PlayerSaveGameOver()
        {
            SaveManager.Instance.SetPlayerLife(data.maxSpaceshipLife);
        }

        private IEnumerator RespawnSpaceshipRoutine()
        {
            yield return new WaitForSeconds(data.respawnDelay);
            SpawnSpaceship(true);
        }

        #endregion
    }
}