using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.Asteroids.Data;

namespace AsteroidsGame.Asteroids
{
    public class AsteroidContext : MonoBehaviour
    {
        [SerializeField]
        private AsteroidData data;

        [SerializeField]
        private Asteroid asteroid;

        public AsteroidData Data => data;

        public Asteroid Asteroid => asteroid;
    }
}