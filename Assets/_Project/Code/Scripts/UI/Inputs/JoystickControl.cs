using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsGame.UI.Inputs
{
    [RequireComponent(typeof(FloatingJoystick))]
    public class JoystickControl : MonoBehaviour
    {
        public static event Action<float, Vector2> RotateSpaceShip;

        private FloatingJoystick joystick;

        #region Unity Methods

        private void Awake()
        {
            joystick = GetComponent<FloatingJoystick>();
        }

        private void Update()
        {
            RotateSpaceship();
        }
        #endregion

        #region Private Methods

        private void RotateSpaceship()
        {
            if (joystick.Direction == Vector2.zero) return;

            var direction = joystick.Direction;
            float angleDeg = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;

            var increaseAngle = (direction.x > 0) ? -90 : 90;

            RotateSpaceShip?.Invoke(angleDeg + increaseAngle, direction);
        }
        #endregion
    }
}
