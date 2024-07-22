using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Pool;

using AsteroidsGame.Bullets;
using AsteroidsGame.CustomVariable;
using AsteroidsGame.UtilWrapper;

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

        private PoolService poolService;

        #region Unity Methods

        private void Start()
        {
            poolService = Services.Get<PoolService>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(bulletTag)) return;

            poolService.Get<DisposeSchedule>(col.transform.position, Quaternion.identity, transform.parent, 0);
            asteroidContextVariable.Value = context;

            col.GetComponent<Bullet>()?.Dispose();
        }

        #endregion        
    }
}
