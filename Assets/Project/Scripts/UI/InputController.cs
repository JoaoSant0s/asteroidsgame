using System.Collections;
using System.Collections.Generic;

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

#region Unity Methods
            private void Awake() 
            {
                SetButtonsActions();
            }
#endregion

            private void SetButtonsActions()
            {
                buttonRotateLeft.HoldEvent.AddListener(()=>
                {
                    RotateSpaceShip?.Invoke(1);
                });

                buttonRotateRight.HoldEvent.AddListener(()=>
                {
                    RotateSpaceShip?.Invoke(-1);
                });

                buttonAccelerate.HoldEvent.AddListener(()=>
                {
                    AccelerateSpaceShip?.Invoke(1);
                });

                buttonShoot.onClick.AddListener(()=>
                {
                    ShootAction?.Invoke();
                });

                buttonHyperSpace.onClick.AddListener(()=>
                {
                    HyperSpaceAction?.Invoke();
                });
            }
    }
}