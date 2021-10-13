using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Data;
using CommonWrapper;

using Main.Scene;
using System;

namespace AsteroidsGame.Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private LevelCollectionData data;

        private int currentLevelIndex = 0;

#region Unity Methods

        private void Awake() 
        {
            AsteroidSpawner.SpawnNextLevel += GoNextLevel; 
        }

        private void OnDestroy() 
        {
            AsteroidSpawner.SpawnNextLevel -= GoNextLevel;
        }

#endregion

        public void Reset()
        {
            AsteroidSpawner.Instance.Reset();
            currentLevelIndex = 0;
        }       

        public IEnumerator GoNextLevelRoutine()
        {
            yield return new WaitForSeconds(data.nextLevelDelay);
            
            SpawnLevelContent();
        }

        public void SpawnLevelContent()
        {
            var level = data.levels[currentLevelIndex];

            for (int i = 0; i < level.Configs.Count; i++)
            {
                var config = level.Configs[i];

                for (int j = 0; j < config.RandomAmount; j++)
                {
                    AsteroidSpawner.Instance.SpawnAsteroid(config.asteroidType);
                }
            }
        }

        private void GoNextLevel()
        {
            currentLevelIndex++;

            if(currentLevelIndex >= data.levels.Count) currentLevelIndex = 0;

            StartCoroutine(GoNextLevelRoutine());
        }
    }
}