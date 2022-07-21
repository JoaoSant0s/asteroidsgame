using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace AsteroidsGame.Animations
{
    public abstract class TweenAnimation : MonoBehaviour
    {
        [Header("Control")]

        [SerializeField]
        protected bool autoRun = false;

        [Header("Sequence Tween Config")]

        [SerializeField]
        protected float delay = 0;

        [SerializeField]
        protected float interval = 0;

        [SerializeField]
        protected int loops = -1;

        [SerializeField]
        protected LoopType loopType;
        
        [SerializeField]
        protected AnimationCurve curve;

        protected Sequence sequence;

        #region Unity Methods

        protected virtual void Start()
        {
            if (autoRun) Run();
        }

        protected virtual void OnDestroy()
        {
            CompleteTween();
        }

        #endregion

        #region Public Methods

        public abstract void Run();
        public abstract void CompleteTween();

        #endregion

        #region Protected Methods
        protected void CancelSequence()
        {
            if (sequence == null) return;

            sequence.Complete();
            sequence.Kill();
        }

        #endregion
    }
}
