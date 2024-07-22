using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.Challenges.Comets
{
    public class ComentMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private CometContext context;

        public void Move(Vector2 target)
        {
            rb.velocity = Vector2.zero;            

            var direction = (target - (Vector2)transform.position).normalized;
            direction.Normalize();

            rb.velocity = direction * context.Data.speed * Time.fixedDeltaTime;
        }
    }
}