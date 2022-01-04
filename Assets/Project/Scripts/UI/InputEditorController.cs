using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Data;

namespace AsteroidsGame.UI
{
    public class InputEditorController : MonoBehaviour
    {
#if UNITY_EDITOR
        public delegate void OnManipulateSpaceShip(int direction);
        public static OnManipulateSpaceShip RotateSpaceShip;
        public static OnManipulateSpaceShip AccelerateSpaceShip;

        public delegate void OnActionSpaceShip();
        public static OnActionSpaceShip ShootAction;
        public static OnActionSpaceShip HyperSpaceAction;

        [Header("Keyboard Map")]

        [SerializeField]
        private SpaceshipKeyboardMapData spaceshipKeyboardMapping;
        #region Unity Methods

        private void Update()
        {
            ListeningKeyboardActions();
        }
        #endregion

        #region Private Methods
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
#endif
    }
}