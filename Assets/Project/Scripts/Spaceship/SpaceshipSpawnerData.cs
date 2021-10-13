using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "SpaceshipSpawnerData", menuName = "AsteroidsGame/SpaceshipSpawnerData")]
    public class SpaceshipSpawnerData : ScriptableObject
    {
        public int maxSpaceshipLife;

        public float respawnDelay;

    }
}