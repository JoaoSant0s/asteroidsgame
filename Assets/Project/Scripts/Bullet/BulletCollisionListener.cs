using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using AsteroidsGame.Unit;
using AsteroidsGame.Data;

namespace AsteroidsGame.Actions
{
    [RequireComponent(typeof(Collider2D))]
    public class BulletCollisionListener : MonoBehaviour
    {
        public delegate void OnBulletshipCollideAsteroid(AsteroidContext context);
        public static OnBulletshipCollideAsteroid AsteroidCollided;
                
        [Tag]
        [SerializeField]
        private string asteroidTag;
        
        [SerializeField]
        private BulletContext context;

        [SerializeField]
        private Bullet bullet;

#region Unity Methods
            void OnTriggerEnter2D(Collider2D col)
            {
                if (!col.CompareTag(asteroidTag)) return;
               
                Instantiate(context.Data.asteroidCollisionEffectPrefab, col.transform.position, Quaternion.identity);
                RegisterBulletCollision(col.GetComponent<AsteroidContext>());
                bullet.Dispose();
            }
#endregion
            private void RegisterBulletCollision(AsteroidContext context)
            {
                AsteroidCollided?.Invoke(context);
            }
    }
}
