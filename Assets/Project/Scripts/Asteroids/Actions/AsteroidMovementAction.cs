using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.UtilWrapper;

namespace AsteroidsGame.Asteroid.Actions
{
    public class AsteroidMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private AsteroidContext context;

#region Unity Methods

        private void OnEnable()
        {
            Move();
        }

#endregion

        private void Move()
        {
            rb.velocity = Vector2.zero;

            var direction = Util.RandomDirection();
            direction.Normalize();

            rb.velocity = direction * context.Data.speed * Time.fixedDeltaTime;
        }
    }
}
