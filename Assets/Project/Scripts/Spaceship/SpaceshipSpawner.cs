using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Main.Scene;
using AsteroidsGame.Unit;
using AsteroidsGame.Data;
using AsteroidsGame.Actions;

namespace AsteroidsGame.Manager
{
    public class SpaceshipSpawner : SceneComponent
    {        
        [SerializeField]
        private Spaceship spaceshipPrefab;

        [SerializeField]
        private SpaceshipSpawnerData data;

        private int spaceshipLife;

        #region Unity Methods

        private void Awake() 
        {
            AsteroidCollisionListener.SpaceshipCollideAsteroid += SpaceshipDestroyed;
        }

        private void OnDestroy() 
        {
            AsteroidCollisionListener.SpaceshipCollideAsteroid -= SpaceshipDestroyed;
        }

        #endregion
        
        public override void Initialize()
        {
            spaceshipLife = data.maxSpaceshipLife;
            RespawnSpaceship();
        }

        private void SpaceshipDestroyed()
        {
            spaceshipLife--;
            Debug.Log(spaceshipLife);

            if(spaceshipLife <= 0) {
                //TODO Game Over
                return;
            }

            StartCoroutine(RespawnSpaceshipRoutine());
        }

        private void RespawnSpaceship()
        {
            Instantiate(spaceshipPrefab, Vector3.zero, Quaternion.identity);
        }

        private IEnumerator RespawnSpaceshipRoutine()
        {
            yield return new WaitForSeconds(data.respawnDelay);
            RespawnSpaceship();
        }
    }
}