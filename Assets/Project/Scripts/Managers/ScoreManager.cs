using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Actions;
using AsteroidsGame.UI.Popup;
using AsteroidsGame.Unit;
using AsteroidsGame.UI;
using System;

namespace AsteroidsGame.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private ScoreCounter scoreLabel;
        private int scorePoints;

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
            scorePoints = newScore;
            UpdateScoreLabel();
        }

        #endregion

        #region Private Methods

        private void BulletshipCollideAsteroid(AsteroidContext context)
        {
            scorePoints += context.Data.destroyScore;
            UpdateScoreLabel();
        }

        private void SaveScore()
        {
            SaveManager.Instance.SetPlayerScore(scorePoints);
        }

        private void UpdateScoreLabel()
        {
            scoreLabel.UpdateScore(string.Format("{0}", scorePoints));
        }

        #endregion
    }
}