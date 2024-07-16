using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Animations;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    [RequireComponent(typeof(Rigidbody2D), typeof(RotateTweenAnimation))]
    public class Comet : ChallengeBehaviour
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
