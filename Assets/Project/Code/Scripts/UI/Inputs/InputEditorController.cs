using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.UI.Data;

namespace AsteroidsGame.UI
{
    public class InputEditorController : MonoBehaviour
    {
#if UNITY_EDITOR
        public static event Action<int> RotateSpaceShip;
        public static event Action<int> AccelerateSpaceShip;
        public static event Action StopAccelerateSpaceShip;
        public static event Action ShootAction;
        public static event Action HyperSpaceAction;

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

            if (Input.GetKeyUp(spaceshipKeyboardMapping.accelerate))
            {
                StopAccelerateSpaceShip?.Invoke();
            }
            else if (Input.GetKey(spaceshipKeyboardMapping.accelerate))
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