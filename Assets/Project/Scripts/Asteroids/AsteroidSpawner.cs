using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Unit;
using AsteroidsGame.Data;
using CommonWrapper;

namespace AsteroidsGame.Collection
{
    public class AsteroidSpawner : SingletonBehaviour<AsteroidSpawner>
    {
        [SerializeField]
        private List<AsteroidTuple> asteroids;

        public void SpawnAsteroid(TupleKeyData type, Vector2 position)
        {
            var asteroid = asteroids.Find( a => a.type == type);
            //Debug.Assert(asteroid.prefab == null, string.Format("Asteroid with type {0} not found", type.name));
            Instantiate(asteroid.prefab, position, Quaternion.identity);
        }
    }

    [Serializable]
    public struct AsteroidTuple
    {
        public TupleKeyData type;
        public Asteroid prefab;
    }
}