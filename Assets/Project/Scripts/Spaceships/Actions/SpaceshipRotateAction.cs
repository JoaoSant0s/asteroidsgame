using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.UI;

namespace AsteroidsGame.Spaceships.Actions
{
    public class SpaceshipRotateAction : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipContext context;

        #region Unity Methods
        private void Awake()
        {
            InputController.RotateSpaceShip += RotateAngle;

#if UNITY_EDITOR
            InputEditorController.RotateSpaceShip += RotateDirection;
#endif
        }

        private void OnDestroy()
        {
            InputController.RotateSpaceShip -= RotateAngle;

#if UNITY_EDITOR
            InputEditorController.RotateSpaceShip -= RotateDirection;
#endif
        }

        #endregion

        private void RotateAngle(float angle)
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
