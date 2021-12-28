using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Data;

namespace AsteroidsGame.Unit
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpaceshipShield : MonoBehaviour
    {
        private SpriteRenderer spriteRender;
        private Coroutine animationRoutine;

        #region Unity Methods

        private void Awake()
        {
            spriteRender = GetComponent<SpriteRenderer>();
        }

        #endregion

        #region Public Methods

        public void StartAnimation(ShieldConfig config)
        {
            StopAnimation();
            animationRoutine = StartCoroutine(StartAnimationRoutine(config));
        }

        public void StopAnimation()
        {
            spriteRender.sprite = null;
            if (animationRoutine == null) return;
            StopCoroutine(animationRoutine);
        }

        #endregion

        #region Private Methods

        private IEnumerator StartAnimationRoutine(ShieldConfig config)
        {
            var index = 0;
            var size = config.sprites.Length;

            while (true)
            {
                spriteRender.sprite = config.sprites[index];
                yield return new WaitForSeconds(config.animationInterval);
                index++;
                index = index % size;
            }
        }

        #endregion

    }
}