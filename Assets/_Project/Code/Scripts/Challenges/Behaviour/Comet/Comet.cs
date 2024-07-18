using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper;

using AsteroidsGame.Animations;
using AsteroidsGame.Spaceships;
using AsteroidsGame.UtilWrapper;

namespace AsteroidsGame.Challenges
{
    [RequireComponent(typeof(Rigidbody2D), typeof(RotateTweenAnimation))]
    public class Comet : ChallengeBehaviour
    {
        private Rigidbody2D rb;
        private RotateTweenAnimation rotateTweenAnimation;
        private ComentMovementAction movementAction;
        private CometContext context;
        private MoveToOppositeSide moveOppositeSide;

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rotateTweenAnimation = GetComponent<RotateTweenAnimation>();
            movementAction = GetComponent<ComentMovementAction>();
            moveOppositeSide = GetComponent<MoveToOppositeSide>();
            context = GetComponent<CometContext>();
        }

        #endregion

        #region Public Methods

        public void Init()
        {
            BuildMoveDirection();
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

        private async void BuildMoveDirection()
        {
            Spaceship spaceship = await SpaceshipSpawner.WaitCurrentSpaceship();
            movementAction.Move(spaceship.Position);
        }

        #endregion

        #region Protected Override Methods

        protected override void OnShow()
        {
            context.Setup();
            rotateTweenAnimation.Run();
            moveOppositeSide.OnPositionChanged += BuildMoveDirection;

            context.OnDamaged += OnDamaged;
            context.OnDestroyed += OnDestroyed;
        }

        protected override void OnDispose()
        {
            rb.velocity = Vector2.zero;
            rotateTweenAnimation.CompleteTween();

            moveOppositeSide.OnPositionChanged -= BuildMoveDirection;

            context.OnDamaged -= OnDamaged;
            context.OnDestroyed -= OnDestroyed;
        }

        #endregion
    }
}
