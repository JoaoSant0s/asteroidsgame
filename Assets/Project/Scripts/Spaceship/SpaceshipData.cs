using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "SpaceshipData", menuName = "AsteroidsGame/SpaceshipData")]
    public class SpaceshipData : ScriptableObject
    {
        [Header("Configs")]
        public float rotateSpeed;        
        public float forwardForce;
        public float maxForwardVelocity;

        [Header("Effects")]
        public GameObject asteroidCollisionEffectPrefab;

    }
}