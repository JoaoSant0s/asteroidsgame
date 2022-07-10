using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.Data;

namespace AsteroidsGame.Unit
{
    public class BulletContext : MonoBehaviour
    {
        [SerializeField]
        private BulletData data;

        public BulletData Data => data;
    }
}