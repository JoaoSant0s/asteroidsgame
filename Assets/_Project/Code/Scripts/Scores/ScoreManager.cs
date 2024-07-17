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

        [SerializeField]
        private IntVariable challengeScoreVariable;

        private PlayerPersistenceService playerPersistence;

        #region Unitye Methods
        protected void Awake()
        {
            asteroidContextVariable.AddChangeListener(BulletshipCollideAsteroid);
            challengeScoreVariable.AddChangeListener(ChallengeScoreIncremented);
            LevelManager.OnSavePlayerScore += SaveScore;
        }        

        private void Start()
        {
            playerPersistence = Services.Get<PlayerPersistenceService>();
        }

        private void OnDestroy()
        {
            asteroidContextVariable.RemoveChangeListener(BulletshipCollideAsteroid);
            challengeScoreVariable.RemoveChangeListener(ChallengeScoreIncremented);
            LevelManager.OnSavePlayerScore -= SaveScore;
        }
        #endregion

        #region Public Methods

        public void SetScore(int newScore)
        {
            scoreVariable.Value = newScore;
        }

        #endregion

        #region Private Methods

        private void BulletshipCollideAsteroid(AsteroidContext _, AsteroidContext newContext)
        {
            scoreVariable.Increment(newContext.Data.destroyScore);
        }

        private void ChallengeScoreIncremented(int _, int newScoreIncrement)
        {
            scoreVariable.Increment(newScoreIncrement);
        }

        private void SaveScore()
        {
            playerPersistence.SetPlayerScore(scoreVariable.Value);
        }

        #endregion
    }
}