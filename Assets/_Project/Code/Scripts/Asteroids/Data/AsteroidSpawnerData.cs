using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.UtilWrapper.Data;

namespace AsteroidsGame.Asteroids.Data
{

    [CreateAssetMenu(fileName = "AsteroidSpawnerData", menuName = "AsteroidsGame/Asteroid/AsteroidSpawnerData")]
    public class AsteroidSpawnerData : ScriptableObject
    {
        public List<AsteroidTuple> asteroidConfigs;

        public int TotalAsteroidsAmount(AsteroidData data)
        {
            var totalSequence = 1;

            if (!data.canSpawnNextAsteroid) return totalSequence;

            for (int i = 0; i < data.nextAsteroidAmount; i++)
            {
                totalSequence += TotalAsteroidsAmount(GetAsteroidData(data.nextAsteroidType));
            }

            return totalSequence;
        }

        private AsteroidData GetAsteroidData(TupleKeyData key)
        {
            var tuple = asteroidConfigs.Find(a => a.type == key);
            return tuple.data;
        }

        public AsteroidData GetAsteroidData(int index)
        {
            var tuple = asteroidConfigs[index];
            return tuple.data;
        }
    }

    [Serializable]
    public struct AsteroidTuple
    {
        public TupleKeyData type;
        public AsteroidData data;

        [Min(0)]
        public int asteroidIndex;
    }
}