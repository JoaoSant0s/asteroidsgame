using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Pool;
using AsteroidsGame.Animations;

namespace AsteroidsGame.Asteroids
{
    [RequireComponent(typeof(Rigidbody2D), typeof(RotateTweenAnimation))]
    public class Asteroid : PoolBase
    {
        private Rigidbody2D rb;
        private RotateTweenAnimation rotateTweenAnimation;

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rotateTweenAnimation = GetComponent<RotateTweenAnimation>();
        }

        #endregion

        #region Protected Override Methods

        protected override void OnShow()
        {
            rotateTweenAnimation.Run();
        }

        protected override void OnDispose()
        {
            rb.velocity = Vector2.zero;
            rotateTweenAnimation.CompleteTween();
        }

        #endregion
    }
}
