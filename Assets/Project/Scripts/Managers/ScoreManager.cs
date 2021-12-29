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
        }

        private void OnDestroy()
        {
            BulletCollisionListener.AsteroidCollided -= BulletshipCollideAsteroid;
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
            SaveManager.Instance.SetPlayerScore(scorePoints);
            UpdateScoreLabel();
        }

        private void UpdateScoreLabel()
        {
            scoreLabel.UpdateScore(string.Format("{0}", scorePoints));
        }

        #endregion
    }
}