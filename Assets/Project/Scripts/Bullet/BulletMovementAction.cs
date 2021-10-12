using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Data;

namespace AsteroidsGame.Actions
{
    public class BulletMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private BulletData data;

        public void Move(Vector2 direction)
        {
            rb.velocity = direction * data.speed * Time.deltaTime;
            Destroy(gameObject, data.lifeTime);
        }
    }
}