using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

using JoaoSant0s.CommonWrapper;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Pool;

using AsteroidsGame.Unit;
using AsteroidsGame.Data;
using AsteroidsGame.Actions;

namespace AsteroidsGame.Manager
{
    public class AsteroidSpawner : SingletonBehaviour<AsteroidSpawner>
    {
        public delegate void OnSpawnNextLevel();
        public static OnSpawnNextLevel SpawnNextLevel;

        public delegate void OnAsteroidAmount(int asteroidsAmount);
        public static OnAsteroidAmount TotalAsteroids;
        public static OnAsteroidAmount CurrentAsteroids;

        [SerializeField]
        [Expandable]
        private AsteroidSpawnerData spawnerData;

        [SerializeField]
        private List<Transform> spawnPoints;

        private int spawnPointIndex;

        private PoolService poolService;

        private List<Asteroid> generatedAsteroids;

        public List<Asteroid> GeneratedAsteroids
        {
            get
            {
                if (generatedAsteroids == null)
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

            BulletCollisionListener.AsteroidCollided += BulletshipCollideAsteroid;
        }

        private void Start()
        {
            poolService = Services.Get<PoolService>();
        }

        private void OnDestroy()
        {
            BulletCollisionListener.AsteroidCollided -= BulletshipCollideAsteroid;
        }

        #endregion

        #region Public Methods
        public void Reset()
        {
            for (int i = 0; i < GeneratedAsteroids.Count; i++)
            {
                GeneratedAsteroids[i].Dispose();
            }
            GeneratedAsteroids.Clear();
        }

        public void SpawnAsteroid(TupleKeyData type)
        {
            var config = spawnerData.asteroidConfigs.Find(a => a.type == type);
            var position = SequencePosition();

            InstantiateAsteroid(config.asteroidIndex, position);
        }

        public void UpdateAsteroidsCounter()
        {
            var total = AsteroidsEstimatedAmount();
            TotalAsteroids?.Invoke(total);
            CurrentAsteroids?.Invoke(total);
        }

        #endregion

        #region Private Methods

        private int AsteroidsEstimatedAmount()
        {
            var total = 0;

            for (int i = 0; i < GeneratedAsteroids.Count; i++)
            {
                total += AsteroidsAmount(GeneratedAsteroids[i]);
            }

            return total;
        }

        private int AsteroidsAmount(Asteroid asteroid)
        {
            var context = asteroid.GetComponent<AsteroidContext>();
            var data = context.Data;

            return AsteroidsAmount(data);
        }

        private int AsteroidsAmount(AsteroidData data)
        {
            var totalSequence = 1;

            if (!data.canSpawnNextAsteroid) return totalSequence;

            for (int i = 0; i < data.nextAsteroidAmount; i++)
            {
                totalSequence += AsteroidsAmount(spawnerData.GetAsteroidData(data.nextAsteroidType));
            }

            return totalSequence;
        }
        private void SpawnAsteroid(TupleKeyData type, Vector2 position)
        {
            var config = spawnerData.asteroidConfigs.Find(a => a.type == type);

            InstantiateAsteroid(config.asteroidIndex, position);
        }

        private void InstantiateAsteroid(int asteroidIndex, Vector3 position)
        {
            var asteroid = poolService.Get<Asteroid>(transform, position, Quaternion.identity, asteroidIndex);

            GeneratedAsteroids.Add(asteroid);
        }

        private Vector3 SequencePosition()
        {
            var position = spawnPoints[spawnPointIndex].position;
            spawnPointIndex++;

            if (spawnPointIndex >= spawnPoints.Count) spawnPointIndex = 0;

            return position + new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0);
        }

        private void BulletshipCollideAsteroid(AsteroidContext context)
        {
            RemoveAsteroid(context.Asteroid);
            SpawnChildrenAsteroids(context.Asteroid, context.Data);
            context.Asteroid.Dispose();

            CurrentAsteroids?.Invoke(AsteroidsEstimatedAmount());

            CheckLevelEnded();
        }

        private void RemoveAsteroid(Asteroid asteroid)
        {
            GeneratedAsteroids.Remove(asteroid);
        }

        private void SpawnChildrenAsteroids(Asteroid asteroid, AsteroidData data)
        {
            if (!data.canSpawnNextAsteroid) return;

            for (int i = 0; i < data.nextAsteroidAmount; i++)
            {
                var position = asteroid.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);

                SpawnAsteroid(data.nextAsteroidType, position);
            }
        }

        private void CheckLevelEnded()
        {
            if (GeneratedAsteroids.Count != 0) return;

            StartCoroutine(SpawnNextLevelRoutine());
        }

        private IEnumerator SpawnNextLevelRoutine()
        {
            yield return new WaitForSeconds(1.5f);

            SpawnNextLevel?.Invoke();
        }

        #endregion
    }
}