using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace AsteroidsGame.Animations
{
    public class RotateTweenAnimation : TweenAnimation
    {
        [Header("Rotate Tween Animation")]

        [SerializeField]
        protected Vector3 startRotate;

        [Header("Tween Config")]

        [SerializeField]
        protected Vector3 endValueRotate = Vector3.one;

        [SerializeField]
        protected float duration = 1;

        [SerializeField]
        protected RotateMode rotateMode = RotateMode.Fast;

        #region Public Override Methods        

        public override void Run()
        {
            CompleteTween();

            sequence = DOTween.Sequence();

            sequence.AppendInterval(delay);
            sequence.Append(transform.DORotate(endValueRotate, duration, rotateMode).SetEase(curve));
            sequence.AppendInterval(interval);
            sequence.SetLoops(loops, loopType);
            sequence.Play();
        }

        public override void CompleteTween()
        {
            CancelSequence();
            transform.localRotation = Quaternion.Euler(startRotate.x, startRotate.y, startRotate.z); ;
        }

        #endregion
    }
}
