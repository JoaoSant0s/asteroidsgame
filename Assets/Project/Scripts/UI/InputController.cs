using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Data;
using AsteroidsGame.UI.Popup;
using JoaoSant0s.CommonWrapper;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsGame.UI
{
    public class InputController : MonoBehaviour
    {
        public delegate void OnManipulateSpaceShip(float angle);
        public static OnManipulateSpaceShip RotateSpaceShip;
        public static OnManipulateSpaceShip AccelerateSpaceShip;

        public delegate void OnActionSpaceShip();
        public static OnActionSpaceShip ShootAction;
        public static OnActionSpaceShip HyperSpaceAction;

        [Header("Joystick")]

        [SerializeField]
        private FloatingJoystick joystick;

        [Header("Hold Buttons")]

        [SerializeField]
        private ButtonHold buttonAccelerate;

        [Header("Simple Buttons")]

        [SerializeField]
        private Button buttonShoot;

        [SerializeField]
        private Button buttonHyperSpace;

        [SerializeField]
        private Button buttonPause;

        private PopupService popupService;


        #region Unity Methods
        private void Awake()
        {
            SetUIButtonsActions();
        }

        private void Start()
        {
            popupService = Services.Get<PopupService>();
        }

        private void Update()
        {
            RotateSpaceship();
        }

        #endregion

        #region Private Methods
        private void SetUIButtonsActions()
        {
            buttonAccelerate.onHoldEvent.AddListener(() =>
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

            buttonPause.onClick.AddListener(() =>
            {
                popupService.Show<PausePopup>();
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