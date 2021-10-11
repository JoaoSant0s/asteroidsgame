using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.UtilWrapper
{
    public class MoveToOppositeSide : MonoBehaviour
    {
        private Canvas canvas;

        private void Awake()
        {
            canvas = FindObjectOfType<Canvas>();
        }

        private void Update()
        {
            var canvasScale = canvas.transform.localScale;
            var limits = new Vector2(Screen.width * canvasScale.x / 2, Screen.height * canvasScale.y  / 2);

            //Debugs.Log(transform.position, limits);
        }
    }
}
