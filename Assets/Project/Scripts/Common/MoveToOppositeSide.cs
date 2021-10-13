using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.UtilWrapper
{
    public class MoveToOppositeSide : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D boxCollider2D;

        private Canvas canvas;

        private Vector2 limits;

        private Vector2 sizeOffset;

#region Unity Methods

        private void Awake()
        {
            canvas = FindObjectOfType<Canvas>();
        }

        private void Start()
        {
            var canvasScale = canvas.transform.localScale;
            
            var rect = ((RectTransform)canvas.transform).rect;

            limits = new Vector2(rect.width * canvasScale.x / 2, rect.height * canvasScale.y / 2);
            sizeOffset = new Vector2(boxCollider2D.size.x / 2, boxCollider2D.size.y / 2);
        }

        private void FixedUpdate()
        {
            Vector2 nextPosition;
            if(!CollideScreenSide(transform.position, out nextPosition)) return;

            transform.position = nextPosition;
        }

#endregion

        private bool CollideScreenSide(Vector2 basePosition, out Vector2 oppositivePosition)
        {
            oppositivePosition = basePosition;

            var offset = 0.1f;

            if (basePosition.x - sizeOffset.x < -limits.x)
            {
                oppositivePosition.x = limits.x - sizeOffset.x - offset;
            }
            else if (basePosition.x + sizeOffset.x > limits.x)
            {
                oppositivePosition.x = -limits.x + sizeOffset.x + offset;
            }
            else if (basePosition.y - sizeOffset.y < -limits.y)
            {
                oppositivePosition.y = limits.y - sizeOffset.y - offset;
            }
            else if (basePosition.y + sizeOffset.y > limits.y)
            {
                oppositivePosition.y = -limits.y + sizeOffset.y + offset;
            }

            return oppositivePosition != basePosition;
        }
    }
}
