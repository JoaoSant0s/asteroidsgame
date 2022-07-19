using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.Bullets.Actions
{
    public class BulletMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private Bullet bullet;

        [SerializeField]
        private BulletContext context;

        public void Move(Vector2 direction)
        {
            rb.velocity = direction * context.Data.speed * Time.fixedDeltaTime;
        }
    }
}