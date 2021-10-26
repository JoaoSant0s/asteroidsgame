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

        public SpaceshipData Data => data;
    }
}