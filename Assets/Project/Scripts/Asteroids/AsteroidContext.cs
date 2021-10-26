using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.Data;

namespace AsteroidsGame.Unit
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