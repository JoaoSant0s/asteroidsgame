using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.Bullets.Data
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "AsteroidsGame/Bullet/BulletData")]
    public class BulletData : ScriptableObject
    {

        [Header("Configs")]
        public float speed;
        public float lifeTime;

        [Header("Effects")]
        public GameObject asteroidCollisionEffectPrefab;

    }
}