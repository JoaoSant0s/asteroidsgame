using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CustomVariable;

using AsteroidsGame.Actions;
using AsteroidsGame.Unit;
using AsteroidsGame.Level;

namespace AsteroidsGame.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        public static event Action<int> OnScoreUpdated;

        [Header("Variables")]
        [SerializeField]
        private IntVariable scoreVariable;

        //private int scorePoints;

        #region Unitye Methods
        protected void Awake()
        {
            BulletCollisionListener.AsteroidCollided += BulletshipCollideAsteroid;
            LevelManager.OnSavePlayerScore += SaveScore;
        }

        private void OnDestroy()
        {
            BulletCollisionListener.AsteroidCollided -= BulletshipCollideAsteroid;
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

        private void BulletshipCollideAsteroid(AsteroidContext context)
        {
            this.scoreVariable.Add(context.Data.destroyScore);
        }

        private void SaveScore()
        {
            SaveManager.Instance.SetPlayerScore(this.scoreVariable.Value);
        }

        #endregion
    }
}