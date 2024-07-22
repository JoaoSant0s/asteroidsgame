using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using JoaoSant0s.ServicePackage.Pool;
using JoaoSant0s.ServicePackage.General;

using AsteroidsGame.UtilWrapper;

namespace AsteroidsGame.Spaceships.Actions
{
    [RequireComponent(typeof(Collider2D))]
    public class SpaceshipCollisionListener : MonoBehaviour
    {
        public static event Action SpaceshipCollided;

        [Tag]
        [SerializeField]
        private string collisionTag;

        [SerializeField]
        private SpriteRenderer spriteRender;

        [SerializeField]
        private SpaceshipContext context;

        private bool collided;
        private float fade;
        private MaterialPropertyBlock materialBlock;

        private PoolService poolService;

        #region Unity Methods      

        private void Awake()
        {
            materialBlock = new();
        }

        private void Start()
        {
            poolService = Services.Get<PoolService>();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (collided) return;
            if (!col.CompareTag(collisionTag)) return;
            if (context.Invulnerable.Value) return;
            collided = true;
            poolService.Get<DisposeSchedule>(col.transform.position, Quaternion.identity, transform.parent, 1);

            RegisterSpaceshipCollision();
            Destroy(gameObject);
            //StartCoroutine(SimulateDestroyFade(col.transform.position));
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
            SpaceshipCollided?.Invoke();
        }
    }
}
