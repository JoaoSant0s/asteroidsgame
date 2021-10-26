using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Unit;
using AsteroidsGame.Data;
using AsteroidsGame.Actions;
using AsteroidsGame.UI.Popup;

using Main.Scene;
using Main.ServicePackage.Popup;
using Main.ServicePackage.General;
using System;

namespace AsteroidsGame.Manager
{
    public class SpaceshipSpawner : MonoBehaviour
    {
        public delegate void OnUpdateSpaceshipLife( int value);
        public static OnUpdateSpaceshipLife UpdateSpaceshipLife;

        [SerializeField]
        private Spaceship spaceshipPrefab;

        [SerializeField]
        private SpaceshipSpawnerData data;

        private int spaceshipLife;

        private PopupService popupService;

#region Unity Methods

        private void Awake() 
        {
            SpaceshipCollisionListener.AsteroidCollided += SpaceshipDestroyed;
        }

        private void Start() 
        {
            popupService = Services.Get<PopupService>();    
        }

        private void OnDestroy() 
        {
            SpaceshipCollisionListener.AsteroidCollided -= SpaceshipDestroyed;
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
            Instantiate(spaceshipPrefab, Vector3.zero, Quaternion.identity);
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
            if(spaceshipLife <= 0) {
                popupService.ShowPopup<GameOverScreenPopup>();
                return;
            }

            StartCoroutine(RespawnSpaceshipRoutine());
        }
    }
}