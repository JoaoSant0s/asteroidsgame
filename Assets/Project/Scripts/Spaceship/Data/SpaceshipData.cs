using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "SpaceshipData", menuName = "AsteroidsGame/Spaceship/SpaceshipData")]
    public class SpaceshipData : ScriptableObject
    {
        [Header("Configs")]
        public float rotateSpeed;        
        public float forwardForce;
        public float maxForwardVelocity;
        public float invulnerabilityDuration;

        [Header("Effects")]
        public GameObject asteroidCollisionEffectPrefab;

    }
}