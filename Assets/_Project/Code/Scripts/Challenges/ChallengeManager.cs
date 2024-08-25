using System;
using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Challenges.Data;
using AsteroidsGame.Levels;

using JoaoSant0s.CommonWrapper;
using JoaoSant0s.Extensions.Collections;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    public class ChallengeManager : MonoBehaviour
    {
        public event Action OnChallengeCompleted;

        [SerializeField]
        private ChallengeCollectionData challengeCollectionData;

        private int sessionLevelProgress = 0;
        private ChallengeObject currenChallengeObject;
        private ChallengeFactory factory;

        public void Init()
        {
            factory = new(this);
        }

        #region Unity Methods        

        private void Start()
        {
            LevelManager.OnLevelSpawned += OnTryCreateChallenge;
            LevelManager.OnLevelCompleted += OnIncrementSessionLevel;
            OnChallengeCompleted += FinishChallenge;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelSpawned -= OnTryCreateChallenge;
            LevelManager.OnLevelCompleted -= OnIncrementSessionLevel;
            OnChallengeCompleted -= FinishChallenge;
        }

        #endregion

        #region Interval Methods

        internal void ChallengeCompleted()
        {
            OnChallengeCompleted?.Invoke();
        }

        #endregion

        #region Private Methods

        private void OnTryCreateChallenge()
        {
            var challangeModule = challengeCollectionData.challengeLevelModule;

            if (sessionLevelProgress < challangeModule || (challangeModule > 0 && sessionLevelProgress % challangeModule != 0))
            {
                return;
            }

            currenChallengeObject?.Clean();

            var challengeIdObject = challengeCollectionData.challenges.Random();
            currenChallengeObject = factory.CreateChallenge(challengeIdObject);
            currenChallengeObject.Init();
        }

        private void OnIncrementSessionLevel()
        {
            FinishChallenge();

            sessionLevelProgress += 1;
        }

        private void FinishChallenge()
        {
            currenChallengeObject?.Clean();
            currenChallengeObject = null;
        }

        #endregion


    }
}
