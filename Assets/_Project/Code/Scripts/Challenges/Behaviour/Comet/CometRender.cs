using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoaoSant0s.CommonWrapper;
using UnityEngine;

namespace AsteroidsGame.Challenges.Comets
{
    public class CometRender : MonoBehaviour
    {
        private MaterialPropertyBlock materialBlock;

        [SerializeField]
        private float shakeForce = 20;

        [SerializeField]
        private int disableShakeDelayInMiliseconds = 1000;

        [SerializeField]
        private SpriteRenderer spriteRender;

        [SerializeField]
        private ParticleSystem tailParticle;

        #region Unity Method

        private void Awake()
        {
            materialBlock = new();
        }

        #endregion

        #region Public Methods

        public void ResetTailPresence()
        {
            tailParticle.Clear();
            tailParticle.Stop();
            tailParticle.Play();
        }

        public async void DamageEffect(float lifeProgress)
        {
            spriteRender.GetPropertyBlock(materialBlock);
            materialBlock.SetFloat("_LifeProgress", lifeProgress);
            materialBlock.SetFloat("_ShakeForce", shakeForce);
            spriteRender.SetPropertyBlock(materialBlock);

            await Task.Delay(disableShakeDelayInMiliseconds);

            spriteRender.GetPropertyBlock(materialBlock);
            materialBlock.SetFloat("_ShakeForce", 0);
            spriteRender.SetPropertyBlock(materialBlock);
        }

        public void Reset()
        {
            spriteRender.GetPropertyBlock(materialBlock);
            materialBlock.SetFloat("_LifeProgress", 1);
            materialBlock.SetFloat("_ShakeForce", 0);
            spriteRender.SetPropertyBlock(materialBlock);
        }

        #endregion
    }
}