using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    public enum ShieldType
    {
        None,
        Invulnerable
    }

    [CreateAssetMenu(fileName = "SpaceshipData", menuName = "AsteroidsGame/Spaceship/SpaceshipData")]
    public class SpaceshipData : ScriptableObject
    {
        [Header("Configs")]

        [Header("Editor")]
        public float rotateSpeed;

        [Header("Gameplay")]
        public float forwardForce;
        public float maxForwardVelocity;
        public float invulnerabilityDuration;

        [Header("Rigidbody")]

        public float linearDrag;
        
        public float angularDrag;

        [Header("Effects")]
        public GameObject asteroidCollisionEffectPrefab;

        [Header("Shields")]
        public ShieldConfig[] shields;

        public ShieldConfig InvulnarableConfig()
        {
            return ShieldConfig(ShieldType.Invulnerable);
        }

        private ShieldConfig ShieldConfig(ShieldType type)
        {
            var config = shields.FirstOrDefault(s => s.type == type);
            return config;
        }
    }

    [Serializable]
    public class ShieldConfig
    {
        public ShieldType type;
        public float animationInterval;
        public Sprite[] sprites;

    }
}