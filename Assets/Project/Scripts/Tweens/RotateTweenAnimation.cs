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

        public Vector3 startRotate;
        public Vector3 endValueRotate = Vector3.one;
        public float duration = 1;
        public RotateMode rotateMode = RotateMode.Fast;

        [Header("Animation Loop")]

        public float delay = 0;
        public float interval = 0;
        public int loops = -1;
        public LoopType loopType;
        public AnimationCurve curve;

        #region Public Methods        

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
