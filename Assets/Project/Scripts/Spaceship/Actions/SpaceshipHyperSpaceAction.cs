using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.UI;
using AsteroidsGame.UtilWrapper;

namespace AsteroidsGame.Actions
{
    public class SpaceshipHyperSpaceAction : MonoBehaviour
    {
        private Vector2 limits;

        #region Unity Methods

        private void Awake()
        {
            InputController.HyperSpaceAction += HyperSpace;
#if UNITY_EDITOR
            InputEditorController.HyperSpaceAction += HyperSpace;
#endif
        }

        private void Start()
        {
            limits = MainCanvas.Instance.Limits;
        }

        private void OnDestroy()
        {
            InputController.HyperSpaceAction -= HyperSpace;
#if UNITY_EDITOR
            InputEditorController.HyperSpaceAction -= HyperSpace;
#endif
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