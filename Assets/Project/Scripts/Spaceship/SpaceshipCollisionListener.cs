using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using AsteroidsGame.Unit;

namespace AsteroidsGame.Actions
{
    [RequireComponent(typeof(Collider2D))]
    public class SpaceshipCollisionListener : MonoBehaviour
    {
        public delegate void OnSpaceshipCollideAsteroid();
        public static OnSpaceshipCollideAsteroid AsteroidCollided;

        [Tag]
        [SerializeField]
        private string asteroidTag;

        [SerializeField]
        private SpaceshipContext context;

        #region Unity Methods      

        void OnTriggerStay2D(Collider2D col)
        {
            if (!col.CompareTag(asteroidTag)) return;
            if (context.IsInvulnerable) return;

            Instantiate(context.Data.asteroidCollisionEffectPrefab, col.transform.position, Quaternion.identity);
            RegisterSpaceshipCollision();
            Destroy(gameObject);
        }

        #endregion

        private void RegisterSpaceshipCollision()
        {
            AsteroidCollided?.Invoke();
        }
    }
}
