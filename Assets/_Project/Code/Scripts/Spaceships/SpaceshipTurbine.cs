using System;
using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.UI;
using AsteroidsGame.UI.Inputs;
using AsteroidsGame.UtilWrapper;
using UnityEngine;

namespace AsteroidsGame.Spaceships
{
    public class SpaceshipTurbine : MonoBehaviour
    {
        [Header("Behaviours")]

        [SerializeField]
        private SpriteRenderer turbineLeft;

        [SerializeField]
        private SpriteRenderer turbineRight;

        [SerializeField]
        private float rotateProportion = 0.75f;

        private int rotateDirectionId;
        private int speedId;

        #region Unity Methods

        private void Awake()
        {
            rotateDirectionId = Shader.PropertyToID("_RotateDirection");
            speedId = Shader.PropertyToID("_Speed");
            //EnableFire(false);
        }        

        private void OnEnable()
        {
            AccelerateButton.AcceleratingSpaceShip += AccelerateDirection;
            AccelerateButton.StopAccelerateSpaceShip += StopTurbine;
            JoystickControl.RotateSpaceShip += RotateAngle;

#if UNITY_EDITOR
            InputEditorController.AccelerateSpaceShip += AccelerateDirection;
            InputEditorController.StopAccelerateSpaceShip += StopTurbine;
            InputEditorController.RotateSpaceShip += RotateDirection;
#endif
        }

        private void OnDisable()
        {
            AccelerateButton.AcceleratingSpaceShip -= AccelerateDirection;
            AccelerateButton.StopAccelerateSpaceShip -= StopTurbine;
            JoystickControl.RotateSpaceShip -= RotateAngle;

#if UNITY_EDITOR
            InputEditorController.AccelerateSpaceShip -= AccelerateDirection;
            InputEditorController.StopAccelerateSpaceShip -= StopTurbine;
            InputEditorController.RotateSpaceShip -= RotateDirection;
#endif
        }

        #endregion

        #region Private Methods        

        private void AccelerateDirection(float direction)
        {
            EnableFire(true);
        }

        private void StopTurbine()
        {
            //EnableFire(false);
        }

        private void RotateAngle(float angle, Vector2 direction)
        {

        }

        private void RotateDirection(int direction)
        {
            Debug.Log(direction);

            var materialBlock = new MaterialPropertyBlock();
            turbineLeft.GetPropertyBlock(materialBlock);
            materialBlock.SetFloat(rotateDirectionId, -direction * rotateProportion);
            turbineLeft.SetPropertyBlock(materialBlock);
        }

        private void EnableFire(bool enable)
        {
            this.turbineLeft.enabled = enable;
            this.turbineRight.enabled = enable;
        }

        #endregion

    }
}