using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.UI;

namespace AsteroidsGame.Spaceships.Actions
{
    public class SpaceshipMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private SpaceshipContext context;

        #region Unity Methods
        private void Awake()
        {
            InputController.AccelerateSpaceShip += AccelerateDirection;
#if UNITY_EDITOR
            InputEditorController.AccelerateSpaceShip += AccelerateDirection;
#endif
        }

        private void Start()
        {
            rb.angularDrag = context.Data.angularDrag;
            rb.drag = context.Data.linearDrag;
        }

        private void OnDestroy()
        {
            InputController.AccelerateSpaceShip -= AccelerateDirection;
#if UNITY_EDITOR
            InputEditorController.AccelerateSpaceShip -= AccelerateDirection;
#endif
        }

        #endregion

        private void AccelerateDirection(float direction)
        {
            AccelerateDirection((int)direction);
        }

        private void AccelerateDirection(int direction)
        {
            if (rb.velocity.magnitude >= context.Data.maxForwardVelocity) return;

            rb.AddForce(transform.up * context.Data.forwardForce * Time.deltaTime);
        }
    }
}