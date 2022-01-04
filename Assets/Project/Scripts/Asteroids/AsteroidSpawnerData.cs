using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.Data
{

    [CreateAssetMenu(fileName = "AsteroidSpawnerData", menuName = "AsteroidsGame/Asteroid/AsteroidSpawnerData")]
    public class AsteroidSpawnerData : ScriptableObject
    {
        public List<AsteroidTuple> asteroidConfigs;

        public AsteroidData GetAsteroidData(TupleKeyData key)
        {
            var tuple = asteroidConfigs.Find(a => a.type == key);
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