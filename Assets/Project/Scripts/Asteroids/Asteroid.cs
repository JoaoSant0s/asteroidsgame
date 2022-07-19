using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Pool;

namespace AsteroidsGame.Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : PoolBase
    {
        private Rigidbody2D rb;

#region Unity Methods

        private void Awake() 
        {
            rb = GetComponent<Rigidbody2D>();
        }

#endregion

        protected override void OnDispose()
        {
            rb.velocity = Vector2.zero;
        }
    }
}
