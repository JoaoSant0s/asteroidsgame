using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Pool;

using AsteroidsGame.Bullets.Actions;
using AsteroidsGame.Animations;

namespace AsteroidsGame.Bullets
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PunchScaleTweenAnimation))]
    public class Bullet : PoolBase
    {
        [SerializeField]
        private BulletMovementAction movementAction;

        [SerializeField]
        private BulletContext context;

        private Rigidbody2D rb;
        private PunchScaleTweenAnimation punchScaleTweenAnimation;

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            punchScaleTweenAnimation = GetComponent<PunchScaleTweenAnimation>();
        }

        #endregion

        #region Protected Override Methods

        protected override void OnShow()
        {
            punchScaleTweenAnimation.Run();
        }

        protected override void OnDispose()
        {
            rb.velocity = Vector2.zero;
            punchScaleTweenAnimation.CompleteTween();

            StopAllCoroutines();
        }

        #endregion

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