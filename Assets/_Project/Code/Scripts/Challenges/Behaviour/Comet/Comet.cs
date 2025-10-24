using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper;

using AsteroidsGame.Animations;
using AsteroidsGame.Spaceships;
using AsteroidsGame.UtilWrapper;

namespace AsteroidsGame.Challenges.Comets
{
    [RequireComponent(typeof(Rigidbody2D), typeof(RotateTweenAnimation))]
    public class Comet : ChallengeBehaviour
    {
        private Rigidbody2D rb;
        private RotateTweenAnimation rotateTweenAnimation;
        private ComentMovementAction movementAction;
        private CometContext context;
        private CometRender render;
        private MoveToOppositeSide moveOppositeSide;

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rotateTweenAnimation = GetComponent<RotateTweenAnimation>();
            movementAction = GetComponent<ComentMovementAction>();
            moveOppositeSide = GetComponent<MoveToOppositeSide>();
            context = GetComponent<CometContext>();
            render = GetComponent<CometRender>();
        }

        #endregion

        #region Public Methods

        public void Init()
        {
            BuildMoveDirection();
        }
        #endregion

        #region Private Methods

        public void OnDamaged(int life, int maxLife)
        {
            render.DamageEffect(life/(float)maxLife);
        }

        private void OnDestroyed()
        {
            Dispose();
        }

        private async void BuildMoveDirection()
        {
            Spaceship spaceship = await SpaceshipSpawner.WaitCurrentSpaceship();
            movementAction.Move(spaceship.Position);
            render.ResetTailPresence();
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
            render.Reset();
            rb.linearVelocity = Vector2.zero;
            rotateTweenAnimation.CompleteTween();

            moveOppositeSide.OnPositionChanged -= BuildMoveDirection;

            context.OnDamaged -= OnDamaged;
            context.OnDestroyed -= OnDestroyed;
        }

        #endregion
    }
}
