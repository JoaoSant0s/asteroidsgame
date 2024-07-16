using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    public class ComentMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private CometContext context;

#region Unity Methods

        private void OnEnable()
        {
            Move();
        }

#endregion

        private void Move()
        {
            rb.velocity = Vector2.zero;

            // TODO: Spawn in the same direction of the player spaceship

            // var direction = Util.RandomDirection();
            // direction.Normalize();

            // rb.velocity = direction * context.Data.speed * Time.fixedDeltaTime;
        }
    }
}