using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "SpaceshipSpawnerData", menuName = "AsteroidsGame/Spaceship/SpaceshipSpawnerData")]
    public class SpaceshipSpawnerData : ScriptableObject
    {
        public int maxSpaceshipLife;
        public float respawnDelay;

        [Header("Reward")]

        public int minRewardLifeLimit;
        public int rewardLifeGain;
    }
}