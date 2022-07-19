using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Pool;

using AsteroidsGame.Bullets.Actions;

namespace AsteroidsGame.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : PoolBase
    {
        [SerializeField]
        private BulletMovementAction movementAction;

        [SerializeField]
        private BulletContext context;
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
            StopAllCoroutines();
        }

        #region Public Methods

        public void Move(Vector2 direction)
        {
            movementAction.Move(direction);
            StartCoroutine(DisposeBullet());
        }

        private IEnumerator DisposeBullet()
        {
            yield return new WaitForSeconds(context.Data.lifeTime);
            Dispose();
        }

        #endregion
    }
}