using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Unit;
using AsteroidsGame.Data;
using AsteroidsGame.Actions;

using CommonWrapper;

namespace AsteroidsGame.Manager
{
    public class AsteroidSpawner : SingletonBehaviour<AsteroidSpawner>
    {
        public delegate void OnSpawnNextLevel();
        public static OnSpawnNextLevel SpawnNextLevel;        

        [SerializeField]
        private List<AsteroidTuple> asteroids;

        [SerializeField]
        private List<Transform> spawnPoints;

        private int spawnPointIndex;

        private List<Asteroid> generatedAsteroids;

        public List<Asteroid> GeneratedAsteroids{
            get
            {
                if(generatedAsteroids == null)
                {
                    generatedAsteroids = new List<Asteroid>();
                }
                return generatedAsteroids;
            }
        }

#region Unity Methods

        protected override void Awake() 
        {
            base.Awake();
            
            AsteroidCollisionListener.BulletCollideAsteroid += BulletshipCollideAsteroid;
        }

        private void OnDestroy() 
        {
            AsteroidCollisionListener.BulletCollideAsteroid -= BulletshipCollideAsteroid;
        }

#endregion       

        public void SpawnAsteroid(TupleKeyData type, Vector2 position)
        {
            var config = asteroids.Find( a => a.type == type);
            
            var asteroid = Instantiate(config.prefab, position, Quaternion.identity);
            GeneratedAsteroids.Add(asteroid);
        }

        public void SpawnAsteroid(TupleKeyData type)
        {
            var config = asteroids.Find( a => a.type == type);
            var position = SequencePosition();

            var asteroid = Instantiate(config.prefab, position, Quaternion.identity);

            GeneratedAsteroids.Add(asteroid);
        }        

        private Vector3 SequencePosition()
        {
            var position = spawnPoints[spawnPointIndex].position;
            spawnPointIndex++;

            if(spawnPointIndex >= spawnPoints.Count) spawnPointIndex = 0;

            return position + new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0);
        }

        private void BulletshipCollideAsteroid(Asteroid asteroid, AsteroidData data)
        {
            RemoveAsteroid(asteroid);
            SpawnChildrenAsteroids(asteroid, data);
            Destroy(asteroid.gameObject);

            CheckLevelEnded();
        }

        private void RemoveAsteroid(Asteroid asteroid)
        {
            GeneratedAsteroids.Remove(asteroid);
        }

        private void SpawnChildrenAsteroids(Asteroid asteroid, AsteroidData data)
        {
            if(!data.canSpawnNextAsteroid) return;

            for (int i = 0; i < data.nextAsteroidAmount; i++)
            {
                var position = asteroid.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);

                AsteroidSpawner.Instance.SpawnAsteroid(data.nextAsteroidType, position);
            }
        } 
        
        private void CheckLevelEnded()
        {
            if(GeneratedAsteroids.Count != 0) return;

            SpawnNextLevel?.Invoke();
        }
    }

    [Serializable]
    public struct AsteroidTuple
    {
        public TupleKeyData type;
        public Asteroid prefab;
    }
}