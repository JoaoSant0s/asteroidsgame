using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

using AsteroidsGame.Manager;
using AsteroidsGame.Data;
using AsteroidsGame.Unit;

namespace AsteroidsGame.Actions
{
    [RequireComponent(typeof(Collider2D))]
    public class AsteroidCollisionListener : MonoBehaviour
    {
        public delegate void OnSpaceshipCollideAsteroid();
        public static OnSpaceshipCollideAsteroid SpaceshipCollideAsteroid;  

        public delegate void OnBulletshipCollideAsteroid(Asteroid asteroid, AsteroidData data);
        public static OnBulletshipCollideAsteroid BulletCollideAsteroid;
        
        [Header("Tags")]
        [Tag]
        [SerializeField]
        private string spaceshipTag;

        [Tag]
        [SerializeField]
        private string bulletTag;

        [Header("Data")]
        [SerializeField]
        private AsteroidData data;

#region Unity Methods        

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == spaceshipTag)
            {
                Instantiate(data.spaceshipCollisionEffectPrefab, col.transform.position, Quaternion.identity);
                Destroy(col.gameObject);
                RegisterSpaceshipCollision();
            }
            else if(col.tag == bulletTag)
            {
                Instantiate(data.bulletCollisionEffectPrefab, col.transform.position, Quaternion.identity);
                RegisterBulletCollision();
                Destroy(col.gameObject);
            }            
        }

#endregion

        private void RegisterSpaceshipCollision()
        {
            SpaceshipCollideAsteroid?.Invoke();
        }

        private void RegisterBulletCollision()
        {
            BulletCollideAsteroid?.Invoke(GetComponent<Asteroid>(), data);
        }
    }
}