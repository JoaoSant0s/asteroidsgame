using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Bullets;
using AsteroidsGame.CustomVariable;
using NaughtyAttributes;
using UnityEngine;

namespace AsteroidsGame.Asteroids.Actions
{
    [RequireComponent(typeof(Collider2D))]
    public class AsteroidCollisionListener : MonoBehaviour
    {
        [Tag]
        [SerializeField]
        private string bulletTag;

        [SerializeField]
        private AsteroidContext context;

        [Header("Variables")]
        [SerializeField]
        private AsteroidContextVariable asteroidContextVariable;

        #region Unity Methods
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(bulletTag)) return;

            Instantiate(context.Data.asteroidCollisionEffectPrefab, col.transform.position, Quaternion.identity);
            asteroidContextVariable.Value = context;

            col.GetComponent<Bullet>()?.Dispose();
        }

        #endregion        
    }
}
