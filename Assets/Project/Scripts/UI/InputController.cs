using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.UI
{
    public class InputController : MonoBehaviour
    {
        public delegate void OnManipulateSpaceShip(int direction);
        public static OnManipulateSpaceShip RotateSpaceShip;
        public static OnManipulateSpaceShip AccelerateSpaceShip;

        [Header("Buttons")]

        [SerializeField]
        private ButtonHold buttonRotateLeft;

        [SerializeField]
        private ButtonHold buttonRotateRight;

        [SerializeField]
        private ButtonHold buttonAccelerate;

#region Unity Methods
            private void Awake() 
            {
                SetButtonsActions();
            }
#endregion

            private void SetButtonsActions()
            {
                buttonRotateLeft.HoldEvent.AddListener(()=>{
                    RotateSpaceShip?.Invoke(1);
                });

                buttonRotateRight.HoldEvent.AddListener(()=>{
                    RotateSpaceShip?.Invoke(-1);
                });

                buttonAccelerate.HoldEvent.AddListener(()=>{
                    AccelerateSpaceShip?.Invoke(1);
                });
            }
            
    }
}