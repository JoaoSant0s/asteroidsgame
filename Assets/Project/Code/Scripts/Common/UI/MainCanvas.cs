using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper;

namespace AsteroidsGame.UtilWrapper
{
    public class MainCanvas : SingletonBehaviour<MainCanvas>
    {
        [SerializeField]
        private Canvas canvas;
        protected override bool IsDontDestroyOnLoad => false;

        public Vector2 Limits
        {
            get
            {
                var canvasScale = canvas.transform.localScale;

                var rect = ((RectTransform)canvas.transform).rect;

                var limits = new Vector2(rect.width * canvasScale.x / 2, rect.height * canvasScale.y / 2);
                return limits;
            }
        }

    }
}