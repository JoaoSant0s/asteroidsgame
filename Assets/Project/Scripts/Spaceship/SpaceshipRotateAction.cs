using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.UI;
using AsteroidsGame.Data;

namespace AsteroidsGame.Actions
{
    public class SpaceshipRotateAction : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipData data;

#region Unity Methods
        private void Awake() 
        {
            InputController.RotateSpaceShip += RotateDirection;
        }

        private void OnDestroy() 
        {
            InputController.RotateSpaceShip -= RotateDirection;
        }

#endregion

        private void RotateDirection(int direction)
        {
            transform.Rotate (0, 0, direction * data.rotateSpeed * Time.deltaTime);            
        }

    }
}
