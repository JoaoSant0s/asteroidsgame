using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.UI;
using AsteroidsGame.Unit;

namespace AsteroidsGame.Actions
{
    public class SpaceshipRotateAction : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipContext context;

        #region Unity Methods
        private void Awake()
        {
            InputController.RotateSpaceShip += RotateAngle;

            InputEditorController.RotateSpaceShip += RotateDirection;
        }

        private void OnDestroy()
        {
            InputController.RotateSpaceShip -= RotateAngle;
            InputEditorController.RotateSpaceShip -= RotateDirection;
        }

        #endregion

        private void RotateAngle(float angle)
        {
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void RotateDirection(int direction)
        {
            transform.Rotate(0, 0, direction * context.Data.rotateSpeed * Time.deltaTime);
        }

    }
}
