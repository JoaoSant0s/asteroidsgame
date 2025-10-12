using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.UI;
using AsteroidsGame.UI.Inputs;

namespace AsteroidsGame.Spaceships.Actions
{
    public class SpaceshipMovementAction : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private SpaceshipContext context;

        #region Unity Methods
        private void OnEnable()
        {
            AccelerateButton.AcceleratingSpaceShip += AccelerateDirection;
            InputController.OnAccelerateSpaceShip += AccelerateDirection;
        }

        private void Start()
        {
            rb.angularDamping = context.Data.angularDrag;
            rb.linearDamping = context.Data.linearDrag;
        }

        private void OnDisable()
        {
            AccelerateButton.AcceleratingSpaceShip -= AccelerateDirection;
            InputController.OnAccelerateSpaceShip -= AccelerateDirection;
        }

        #endregion

        private void AccelerateDirection(float _)
        {
            if (rb.linearVelocity.magnitude >= context.Data.maxForwardVelocity) return;

            rb.AddForce(transform.up * context.Data.forwardForce * Time.deltaTime);
        }
    }
}