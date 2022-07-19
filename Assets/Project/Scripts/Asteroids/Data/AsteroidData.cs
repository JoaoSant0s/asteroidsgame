using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using AsteroidsGame.Data;

namespace AsteroidsGame.Asteroid.Data
{
    [CreateAssetMenu(fileName = "AsteroidData", menuName = "AsteroidsGame/Asteroid/AsteroidData")]
    public class AsteroidData : ScriptableObject
    {
        [Header("Configs")]
        public float speed;
        public int destroyScore;

        [Header("Asteroid Children")]
        public bool canSpawnNextAsteroid;

        [ShowIf(nameof(canSpawnNextAsteroid))]
        public int nextAsteroidAmount;
        
        [ShowIf(nameof(canSpawnNextAsteroid))]
        public TupleKeyData nextAsteroidType;
    }
}