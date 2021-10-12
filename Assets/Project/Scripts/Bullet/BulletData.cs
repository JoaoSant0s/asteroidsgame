using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "AsteroidsGame/BulletData")]
    public class BulletData : ScriptableObject
    {
        public float speed;
        public float lifeTime;
    }
}