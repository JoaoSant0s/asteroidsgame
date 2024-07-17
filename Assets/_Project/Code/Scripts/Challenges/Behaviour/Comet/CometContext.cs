using System;
using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Challenges.Data;
using JoaoSant0s.CommonWrapper;
using JoaoSant0s.CustomVariable;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    public class CometContext : MonoBehaviour
    {
        public event Action<int> OnDamaged;
        public event Action OnDestroyed;

        [SerializeField]
        private CometData data;

        [SerializeField]
        private IntVariable challengeScoreVariable;
        public CometData Data => data;
        
        private int life;        

        #region Public Methods

        public void Setup()
        {
            life = data.life;
        }

        public void Damaged()
        {
            if(life <= 0) return;

            life -= 1;
            OnDamaged?.Invoke(life);

            if (life <= 0) {
                challengeScoreVariable.Value = data.destroyScore;                
                OnDestroyed?.Invoke();
            }
        }

        #endregion
    }
}