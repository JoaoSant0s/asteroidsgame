using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.InputSystem;

namespace AsteroidsGame.UI
{
    public class InputController : MonoBehaviour
    {
        public static event Action<int> OnRotateSpaceShip;
        public static event Action<float> OnAccelerateSpaceShip;
        public static event Action OnStopAccelerateSpaceShip;
        public static event Action OnShootAction;
        public static event Action OnHyperSpaceAction;

        private InputControls input;

        #region Unity Methods

        private void Awake()
        {
            input = new InputControls();
        }

        private void OnEnable()
        {
            input.Spaceship.Enable();

            input.Spaceship.Accelerate.canceled += StopAccelerateSpaceShip;

            input.Spaceship.ShootAction.performed += ShootAction;
            input.Spaceship.HyperSpaceAction.performed += HyperSpaceAction;
        }

        private void Update()
        {
            RegisteringHoldActions();
        }

        private void OnDisable()
        {
            input.Spaceship.Disable();

            input.Spaceship.Accelerate.canceled -= StopAccelerateSpaceShip;

            input.Spaceship.ShootAction.performed -= ShootAction;
            input.Spaceship.HyperSpaceAction.performed -= HyperSpaceAction;
        }
        #endregion

        #region Private Methods

        private void ShootAction(InputAction.CallbackContext context)
        {
            OnShootAction?.Invoke();
        }

        private void HyperSpaceAction(InputAction.CallbackContext context)
        {
            OnHyperSpaceAction?.Invoke();
        }

        private void StopAccelerateSpaceShip(InputAction.CallbackContext context)
        {
            OnStopAccelerateSpaceShip?.Invoke();
        }

        private void RegisteringHoldActions()
        {
            if (input.Spaceship.Accelerate.IsPressed())
            {
                OnAccelerateSpaceShip?.Invoke(1);
            }

            if (input.Spaceship.RotateLeft.IsPressed())
            {
                OnRotateSpaceShip?.Invoke(1);
            }

            if (input.Spaceship.RotateRight.IsPressed())
            {
                OnRotateSpaceShip?.Invoke(-1);
            }
        }

        #endregion
    }
}