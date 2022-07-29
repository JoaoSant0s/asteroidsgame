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
        private SpaceshipTurbine turbine;

        [SerializeField]
        private SpaceshipContext context;

        #region Unity Methods
        private void OnEnable()
        {
            AccelerateButton.AcceleratingSpaceShip += AccelerateDirection;
            AccelerateButton.StopAccelerateSpaceShip += StopTurbine;
#if UNITY_EDITOR
            InputEditorController.AccelerateSpaceShip += AccelerateDirection;
            InputEditorController.StopAccelerateSpaceShip += StopTurbine;
#endif
        }

        private void Start()
        {
            rb.angularDrag = context.Data.angularDrag;
            rb.drag = context.Data.linearDrag;
        }

        private void OnDisable()
        {
            AccelerateButton.AcceleratingSpaceShip -= AccelerateDirection;
            AccelerateButton.StopAccelerateSpaceShip -= StopTurbine;
#if UNITY_EDITOR
            InputEditorController.AccelerateSpaceShip -= AccelerateDirection;
            InputEditorController.StopAccelerateSpaceShip -= StopTurbine;
#endif
        }

        #endregion

        private void AccelerateDirection(float direction)
        {
            AccelerateDirection((int)direction);
        }

        private void AccelerateDirection(int direction)
        {
            this.turbine.EnableFire(true);
            if (rb.velocity.magnitude >= context.Data.maxForwardVelocity) return;

            rb.AddForce(transform.up * context.Data.forwardForce * Time.deltaTime);
        }

        private void StopTurbine()
        {
            this.turbine.EnableFire(false);
        }
    }
}