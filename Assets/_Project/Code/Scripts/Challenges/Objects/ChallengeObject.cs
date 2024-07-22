using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    public abstract class ChallengeObject
    {
        public event Action OnChallengeCompleted;

        protected ChallengeManager manager;
        public ChallengeObject(ChallengeManager newManager)
        {
            manager = newManager;
        }
        public abstract void Init();
        public abstract void Clean();

        protected void ChallengeCompleted()
        {
            OnChallengeCompleted?.Invoke();
        }
    }
}
