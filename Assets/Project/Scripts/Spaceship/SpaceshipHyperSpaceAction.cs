using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.UI;
using UnityEngine;

namespace AsteroidsGame.Actions
{
    public class SpaceshipHyperSpaceAction : MonoBehaviour
    {
        private Canvas canvas;

       
#region Unity Methods

        private void Awake()
        {
            canvas = FindObjectOfType<Canvas>();

            InputController.HyperSpaceAction += HyperSpace;
        }

        private void OnDestroy()
        {
            InputController.HyperSpaceAction -= HyperSpace;
        }

#endregion

        private void HyperSpace()
        {
            var canvasScale = canvas.transform.localScale;
            var limits = new Vector2(Screen.width * canvasScale.x / 2, Screen.height * canvasScale.y / 2);

            var xPosition = Random.Range(-limits.x, limits.x);
            var yPosition = Random.Range(-limits.y, limits.y);

            var newPosition = new Vector3(xPosition, yPosition, transform.position.z);

            transform.position = newPosition;
        }
    }
}