using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.CommonWrapper;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;

using AsteroidsGame.UI.Popup;

namespace AsteroidsGame.UI
{
    public class InputController : MonoBehaviour
    {
        public static event Action<float> RotateSpaceShip;

        public static event Action ShootAction;
        public static event Action HyperSpaceAction;

        [Header("Joystick")]

        [SerializeField]
        private FloatingJoystick joystick;

        [Header("Hold Buttons")]

        [Header("Simple Buttons")]

        [SerializeField]
        private Button buttonShoot;

        [SerializeField]
        private Button buttonHyperSpace;

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