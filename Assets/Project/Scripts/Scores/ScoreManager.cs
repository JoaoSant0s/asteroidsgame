using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CustomVariable;
using JoaoSant0s.ServicePackage.General;

using AsteroidsGame.Levels;
using AsteroidsGame.CustomVariable;
using AsteroidsGame.Save;
using AsteroidsGame.Asteroids;

namespace AsteroidsGame.Scores
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField]
        private IntVariable scoreVariable;

        [SerializeField]
        private AsteroidContextVariable asteroidContextVariable;

        private PlayerPersistenceService playerPersistence;

        #region Unitye Methods
        protected void Awake()
        {
            this.asteroidContextVariable.OnValueModified += BulletshipCollideAsteroid;
            LevelManager.OnSavePlayerScore += SaveScore;
        }

        private void Start()
        {
            playerPersistence = Services.Get<PlayerPersistenceService>();
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
            playerPersistence.SetPlayerScore(this.scoreVariable.Value);
        }

        #endregion
    }
}