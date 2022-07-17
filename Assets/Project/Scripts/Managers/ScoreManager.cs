using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CustomVariable;

using AsteroidsGame.Actions;
using AsteroidsGame.Unit;
using AsteroidsGame.Level;
using AsteroidsGame.CustomVariable;

namespace AsteroidsGame.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField]
        private IntVariable scoreVariable;

        [SerializeField]
        private AsteroidContextVariable asteroidContextVariable;

        #region Unitye Methods
        protected void Awake()
        {
            this.asteroidContextVariable.OnValueModified += BulletshipCollideAsteroid;
            LevelManager.OnSavePlayerScore += SaveScore;
        }

        private void OnDestroy()
        {
            this.asteroidContextVariable.OnValueModified -= BulletshipCollideAsteroid;
            LevelManager.OnSavePlayerScore -= SaveScore;
        }
        #endregion

        #region Public Methods

        public void SetScore(int newScore)
        {
            this.scoreVariable.Modify(newScore);
        }

        #endregion

        #region Private Methods

        private void BulletshipCollideAsteroid(AsteroidContext previousContext, AsteroidContext newContext)
        {
            this.scoreVariable.Add(newContext.Data.destroyScore);
        }

        private void SaveScore()
        {
            SaveManager.Instance.SetPlayerScore(this.scoreVariable.Value);
        }

        #endregion
    }
}