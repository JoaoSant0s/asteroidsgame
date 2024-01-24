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
#if UNITY_EDITOR
            InputEditorController.AccelerateSpaceShip += AccelerateDirection;
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
#if UNITY_EDITOR
            InputEditorController.AccelerateSpaceShip -= AccelerateDirection;
#endif
        }

        #endregion

        private void AccelerateDirection(float _)
        {
            if (rb.velocity.magnitude >= context.Data.maxForwardVelocity) return;

            rb.AddForce(transform.up * context.Data.forwardForce * Time.deltaTime);
        }
    }
}