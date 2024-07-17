using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Animations;
using JoaoSant0s.CommonWrapper;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    [RequireComponent(typeof(Rigidbody2D), typeof(RotateTweenAnimation))]
    public class Comet : ChallengeBehaviour
    {
        private Rigidbody2D rb;
        private RotateTweenAnimation rotateTweenAnimation;
        private ComentMovementAction movementAction;
        private CometContext context;

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rotateTweenAnimation = GetComponent<RotateTweenAnimation>();
            movementAction = GetComponent<ComentMovementAction>();
            context = GetComponent<CometContext>();
        }

        #endregion

        #region Public Methods

        public void Init(Vector3 target)
        {
            movementAction.Move(target);
            context.OnDamaged += OnDamaged;
            context.OnDestroyed += OnDestroyed;
        }
        #endregion

        #region Private Methods

        public void OnDamaged(int life)
        {
            Debugs.Log("Damaged", life);
            // TODO: Apply Effect here
        }

        private void OnDestroyed()
        {
            // TODO: Play Destruction Effect here
            Dispose();
        }

        #endregion

        #region Protected Override Methods

        protected override void OnShow()
        {
            context.Setup();
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
