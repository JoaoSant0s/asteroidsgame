using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Data;
using JoaoSant0s.CommonWrapper;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsGame.UI
{
    public class InputController : MonoBehaviour
    {
        public delegate void OnManipulateSpaceShip(float direction);
        public static OnManipulateSpaceShip RotateSpaceShip;
        public static OnManipulateSpaceShip AccelerateSpaceShip;

        public delegate void OnActionSpaceShip();
        public static OnActionSpaceShip ShootAction;
        public static OnActionSpaceShip HyperSpaceAction;

        [Header("Joystick")]

        [SerializeField]
        private FixedJoystick joystick;

        [Header("Hold Buttons")]       

        [SerializeField]
        private ButtonHold buttonAccelerate;

        [Header("Simple Buttons")]

        [SerializeField]
        private Button buttonShoot;

        [SerializeField]
        private Button buttonHyperSpace;

        [Header("Keyboard Map")]

        [SerializeField]
        private SpaceshipKeyboardMapData spaceshipKeyboardMapping;

        #region Unity Methods
        private void Awake()
        {
            SetUIButtonsActions();
        }

        private void Update()
        {
            RotateSpaceship();
        }

        #endregion

        #region Private Methods
        private void SetUIButtonsActions()
        {
            buttonAccelerate.HoldEvent.AddListener(() =>
            {
                AccelerateSpaceShip?.Invoke(1);
            });

            buttonShoot.onClick.AddListener(() =>
            {
                ShootAction?.Invoke();
            });

            buttonHyperSpace.onClick.AddListener(() =>
            {
                HyperSpaceAction?.Invoke();
            });
        }

        private void RotateSpaceship()
        {
            if (joystick.Direction == Vector2.zero) return;

            var direction = joystick.Direction;
            float angleDeg = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;

            var increaseAngle = (direction.x > 0) ? -90 : 90;

            RotateSpaceShip?.Invoke(angleDeg + increaseAngle);
        }

        #endregion
    }
}