using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.UI;
using AsteroidsGame.Unit;

namespace AsteroidsGame.Actions
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
        }

        private void OnDestroy() 
        {
            InputController.AccelerateSpaceShip -= AccelerateDirection;
        }

#endregion

        private void AccelerateDirection(float direction)
        {
            if(rb.velocity.magnitude >= context.Data.maxForwardVelocity) return;

            rb.AddForce(transform.up * context.Data.forwardForce * Time.deltaTime);
        }
    }
}