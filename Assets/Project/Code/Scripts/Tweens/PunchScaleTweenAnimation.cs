using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace AsteroidsGame.Animations
{
    public class PunchScaleTweenAnimation : TweenAnimation
    {
        [Header("PunchScale Tween Animation")]

        [SerializeField]
        protected Vector3 startScale = Vector3.one;

        [Header("Tween Config")]

        [SerializeField]
        protected Vector3 punch = Vector3.one;

        [SerializeField]
        protected float scale = 1;

        [SerializeField]
        protected float time = .5f;

        [SerializeField]
        protected int vibrato = 10;

        [SerializeField]
        protected float elasticity = 1;

        #region Public Override Methods        

        public override void Run()
        {
            CompleteTween();

            sequence = DOTween.Sequence();

            sequence.AppendInterval(delay);
            sequence.Append(transform.DOPunchScale(punch * scale, time, vibrato, elasticity).SetEase(curve));
            sequence.AppendInterval(interval);
            sequence.SetLoops(loops, loopType);
            sequence.Play();
        }
        public override void CompleteTween()
        {
            CancelSequence();
            transform.localScale = startScale;
        }


        #endregion

    }
}
