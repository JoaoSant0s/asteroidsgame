using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.Data;

namespace AsteroidsGame.Unit
{
    public class SpaceshipContext : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipData data;

        [SerializeField]
        private Spaceship currentSpaceship;

        public SpaceshipData Data => data;

        public bool IsInvulnerable => currentSpaceship.IsInvulnerable;
    }
}