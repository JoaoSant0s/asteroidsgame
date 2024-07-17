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
        [SerializeField]
        private ChallengeCollectionData challengeCollectionData;

        private int sessionLevelProgress = 0;
        private ChallengeObject currenChallengeObject;
        private ChallengeFactory factory;

        public void Init()
        {
            factory = new();
        }

        #region Unity Methods        

        private void Start()
        {
            LevelManager.OnLevelSpawned += OnTryCreateChallenge;
            LevelManager.OnLevelCompleted += OnIncrementSessionLevel;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelSpawned -= OnTryCreateChallenge;
            LevelManager.OnLevelCompleted -= OnIncrementSessionLevel;
        }

        #endregion

        #region Private Methods

        private void OnTryCreateChallenge()
        {
            var challangeModule = challengeCollectionData.challengeLevelModule;

            if (sessionLevelProgress < challangeModule || sessionLevelProgress % challangeModule != 0)
            {
                return;
            }

            var currentChallengeType = challengeCollectionData.challengeTypes.Random();
            currenChallengeObject?.Clean();

            currenChallengeObject = factory.CreateChallenge(currentChallengeType);
            currenChallengeObject.Init();
        }

        private void OnIncrementSessionLevel()
        {
            currenChallengeObject?.Clean();
            currenChallengeObject = null;
            
            sessionLevelProgress += 1;
        }

        #endregion


    }
}
