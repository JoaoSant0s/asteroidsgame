using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Data;
using AsteroidsGame.UI;

namespace AsteroidsGame.Actions
{
    public class SpaceshipMovementAction : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private SpaceshipData data;

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

        private void AccelerateDirection(int direction)
        {
            if(rb.velocity.magnitude >= data.maxForwardVelocity) return;

            rb.AddForce(transform.up * data.forwardForce * Time.deltaTime);
        }
    }
}