using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Pool;

namespace AsteroidsGame.Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : PoolBase
    {
        private PoolService poolService;
        private Rigidbody2D rb;

#region Unity Methods

        private void Awake() 
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start() 
        {
            poolService = Services.Get<PoolService>();
        }

#endregion

        public override void OnDispose()
        {
            rb.velocity = Vector2.zero;
            StopAllCoroutines();
        }
    }
}