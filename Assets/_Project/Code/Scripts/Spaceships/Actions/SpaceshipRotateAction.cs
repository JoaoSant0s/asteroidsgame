using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.UI;
using AsteroidsGame.UI.Inputs;

namespace AsteroidsGame.Spaceships.Actions
{
    public class SpaceshipRotateAction : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipContext context;

        #region Unity Methods
        private void Awake()
        {
            JoystickControl.RotateSpaceShip += RotateAngle;
            InputController.OnRotateSpaceShip += RotateDirection;
        }

        private void OnDestroy()
        {
            JoystickControl.RotateSpaceShip -= RotateAngle;
            InputController.OnRotateSpaceShip -= RotateDirection;
        }

        #endregion

        private void RotateAngle(float angle, Vector2 _)
        {
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
#if UNITY_EDITOR

        private void RotateDirection(int direction)
        {
            transform.Rotate(0, 0, direction * context.Data.rotateSpeed * Time.deltaTime);
        }
#endif

    }
}
