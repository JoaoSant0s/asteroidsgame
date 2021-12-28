using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsGame.UI
{
    public class InputController : MonoBehaviour
    {
        public delegate void OnManipulateSpaceShip(int direction);
        public static OnManipulateSpaceShip RotateSpaceShip;
        public static OnManipulateSpaceShip AccelerateSpaceShip;

        public delegate void OnActionSpaceShip();
        public static OnActionSpaceShip ShootAction;
        public static OnActionSpaceShip HyperSpaceAction;

        [Header("Hold Buttons")]

        [SerializeField]
        private ButtonHold buttonRotateLeft;

        [SerializeField]
        private ButtonHold buttonRotateRight;

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

#if UNITY_EDITOR
        private void Update()
        {
            ListeningKeyboardActions();
        }
#endif

        #endregion

        #region Private Methods
        private void SetUIButtonsActions()
        {
            buttonRotateLeft.HoldEvent.AddListener(() =>
            {
                RotateSpaceShip?.Invoke(1);
            });

            buttonRotateRight.HoldEvent.AddListener(() =>
            {
                RotateSpaceShip?.Invoke(-1);
            });

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

        private void ListeningKeyboardActions()
        {
            if (Input.GetKeyUp(spaceshipKeyboardMapping.shootAction))
            {
                ShootAction?.Invoke();
            }

            if (Input.GetKeyUp(spaceshipKeyboardMapping.hyperSpaceAction))
            {
                HyperSpaceAction?.Invoke();
            }

            if (Input.GetKey(spaceshipKeyboardMapping.accelerate))
            {
                AccelerateSpaceShip?.Invoke(1);
            }

            if (Input.GetKey(spaceshipKeyboardMapping.rotateLeft))
            {
                RotateSpaceShip?.Invoke(1);
            }

            if (Input.GetKey(spaceshipKeyboardMapping.rotateRight))
            {
                RotateSpaceShip?.Invoke(-1);
            }
        }

        #endregion
    }
}