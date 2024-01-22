using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Bullets.Data;

namespace AsteroidsGame.Bullets
{
    public class BulletContext : MonoBehaviour
    {
        [SerializeField]
        private BulletData data;

        public BulletData Data => data;
    }
}