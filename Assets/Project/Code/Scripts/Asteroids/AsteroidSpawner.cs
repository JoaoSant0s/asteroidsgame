using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Pool;
using JoaoSant0s.CustomVariable;

using AsteroidsGame.Asteroids.Data;
using AsteroidsGame.CustomVariable;
using AsteroidsGame.Levels;
using AsteroidsGame.UtilWrapper.Data;

namespace AsteroidsGame.Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        public static event Action SpawnNextLevel;

        [Header("Variables")]
        [SerializeField]
        private IntVariable totalAsteroidsVariable;

        [SerializeField]
        private IntVariable currentAsteroidsVariable;

        [SerializeField]
        private AsteroidContextVariable asteroidContextVariable;

        [Header("Data")]
        [SerializeField]
        [Expandable]
        private AsteroidSpawnerData spawnerData;

        [Header("References")]

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

        private void Awake()
        {
            this.asteroidContextVariable.OnValueModified += BulletshipCollideAsteroid;
            LevelManager.OnLevelSpawned += UpdateAsteroidsCounter;
            LevelManager.OnSpawnAsteroid += SpawnAsteroid;
        }

        private void Start()
        {
            poolService = Services.Get<PoolService>();
        }

        private void OnDestroy()
        {
            this.asteroidContextVariable.OnValueModified -= BulletshipCollideAsteroid;
            LevelManager.OnLevelSpawned -= UpdateAsteroidsCounter;
            LevelManager.OnSpawnAsteroid -= SpawnAsteroid;
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

        #endregion

        #region Private Methods

        private void SpawnAsteroid(TupleKeyData type)
        {
            var config = spawnerData.asteroidConfigs.Find(a => a.type == type);
            var position = SequencePosition();

            InstantiateAsteroid(config.asteroidIndex, position);
        }

        private void UpdateAsteroidsCounter()
        {
            var total = AsteroidsEstimatedAmount();
            this.totalAsteroidsVariable.Modify(total);
            this.currentAsteroidsVariable.Modify(total);
        }

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

            return AsteroidsAmount(context.Data);
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
            var asteroid = poolService.Get<Asteroid>(position, Quaternion.identity, transform, asteroidIndex);

            GeneratedAsteroids.Add(asteroid);
        }

        private Vector3 SequencePosition()
        {
            var position = spawnPoints[spawnPointIndex].position;

            spawnPointIndex = ++spawnPointIndex % spawnPoints.Count;

            return position + new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0);
        }

        private void BulletshipCollideAsteroid(AsteroidContext previousContext, AsteroidContext newContext)
        {
            RemoveAsteroid(newContext.Asteroid);
            SpawnChildrenAsteroids(newContext.Asteroid, newContext.Data);
            newContext.Asteroid.Dispose();

            var estimatedAsteroidsAmount = AsteroidsEstimatedAmount();
            this.currentAsteroidsVariable.Modify(estimatedAsteroidsAmount);

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