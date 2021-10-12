using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Data;
using AsteroidsGame.UtilWrapper;

namespace AsteroidsGame.Actions
{
    public class AsteroidMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private AsteroidData data;

#region Unity Methods

        private void Start()
        {
            Move();
        }

#endregion

        private void Move()
        {
            rb.velocity = Vector2.zero;

            var direction = Util.RandomDirection();
            direction.Normalize();

            rb.velocity = direction * data.speed * Time.deltaTime;
        }
    }
}
