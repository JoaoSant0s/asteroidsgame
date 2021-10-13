using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "AsteroidData", menuName = "AsteroidsGame/AsteroidData")]
    public class AsteroidData : ScriptableObject
    {
        public float speed;
        public int destroyScore;

        public bool canSpawnNextAsteroid;

        [ShowIf(nameof(canSpawnNextAsteroid))]
        public int nextAsteroidAmount;
        
        [ShowIf(nameof(canSpawnNextAsteroid))]
        public TupleKeyData nextAsteroidType;
    }
}