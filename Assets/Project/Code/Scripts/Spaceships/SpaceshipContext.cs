using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JoaoSant0s.CustomVariable;

using AsteroidsGame.Spaceships.Data;

namespace AsteroidsGame.Spaceships
{
    public class SpaceshipContext : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipData data;

        public SpaceshipData Data => data;
        public BoolVariable Invulnerable;
    }
}