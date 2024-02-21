using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

namespace AsteroidsGame.Spaceships.Actions
{
    [RequireComponent(typeof(Collider2D))]
    public class SpaceshipCollisionListener : MonoBehaviour
    {
        public static event Action AsteroidCollided;

        [Tag]
        [SerializeField]
        private string asteroidTag;

        [SerializeField]
        private SpriteRenderer spriteRender;

        [SerializeField]
        private SpaceshipContext context;

        private bool collided;
        private float fade;
        private MaterialPropertyBlock materialBlock;

        #region Unity Methods      

        private void Awake()
        {
            materialBlock = new();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (collided) return;
            if (!col.CompareTag(asteroidTag)) return;
            if (context.Invulnerable.Value) return;
            collided = true;

            StartCoroutine(SimulateDestroyFade(col.transform.position));
        }

        #endregion

        private IEnumerator SimulateDestroyFade(Vector3 position)
        {
            fade = 0;
            while (fade < 1)
            {
                fade += Time.deltaTime;
                spriteRender.GetPropertyBlock(materialBlock);
                materialBlock.SetFloat("_Fade", fade);
                spriteRender.SetPropertyBlock(materialBlock);
                yield return null;
            }

            Instantiate(context.Data.asteroidCollisionEffectPrefab, position, Quaternion.identity);
            RegisterSpaceshipCollision();
            Destroy(gameObject);
        }

        private void RegisterSpaceshipCollision()
        {
            AsteroidCollided?.Invoke();
        }
    }
}
