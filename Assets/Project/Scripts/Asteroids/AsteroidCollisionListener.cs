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
                //TODO
            }
            else if(col.tag == bulletTag)
            {
                AsteroidSpawner.Instance.RemoveAsteroid(GetComponent<Asteroid>());
                SpawnNextAsteroid();

                Destroy(col.gameObject);
                Destroy(gameObject);

                AsteroidSpawner.Instance.CheckLevelEnded();
            }            
        }

#endregion

        private void SpawnNextAsteroid()
        {
            if(!data.canSpawnNextAsteroid) return;

            for (int i = 0; i < data.nextAsteroidAmount; i++)
            {
                var position = transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);

                AsteroidSpawner.Instance.SpawnAsteroid(data.nextAsteroidType, position);
            }
        }
    }
}