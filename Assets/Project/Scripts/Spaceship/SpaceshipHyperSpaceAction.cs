using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.UI;
using UnityEngine;

namespace AsteroidsGame.Actions
{
    public class SpaceshipHyperSpaceAction : MonoBehaviour
    {
        private Canvas canvas;

        private Vector2 limits;

       
#region Unity Methods

        private void Awake()
        {
            canvas = FindObjectOfType<Canvas>();

            InputController.HyperSpaceAction += HyperSpace;
        }

        private void Start() 
        {
            var canvasScale = canvas.transform.localScale;
            var rect = ((RectTransform)canvas.transform).rect;

            limits = new Vector2(rect.width * canvasScale.x / 2, rect.height * canvasScale.y / 2);
        }

        private void OnDestroy()
        {
            InputController.HyperSpaceAction -= HyperSpace;
        }

#endregion

        private void HyperSpace()
        {
            var xPosition = Random.Range(-limits.x, limits.x);
            var yPosition = Random.Range(-limits.y, limits.y);

            var newPosition = new Vector3(xPosition, yPosition, transform.position.z);

            transform.position = newPosition;
        }
    }
}