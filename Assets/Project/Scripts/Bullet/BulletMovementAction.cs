using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Unit;

namespace AsteroidsGame.Actions
{
    public class BulletMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private BulletContext context;

        public void Move(Vector2 direction)
        {
            rb.velocity = direction * context.Data.speed * Time.deltaTime;
            Destroy(gameObject, context.Data.lifeTime);
        }
    }
}