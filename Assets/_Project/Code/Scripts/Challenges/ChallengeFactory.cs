using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Challenges.Data;

namespace AsteroidsGame.Challenges
{
    public class ChallengeFactory
    {
        public delegate ChallengeObject OnCreateChallenge();

        private Dictionary<ChallengeType, OnCreateChallenge> challengeFactory;

        public ChallengeFactory(ChallengeManager manager)
        {
            challengeFactory = new()
            {
                {
                    ChallengeType.Comet,
                    () => { return new ChallengeComet(manager); }
                }
            };
        }

        public ChallengeObject CreateChallenge(ChallengeType challengeType)
        {
            return challengeFactory[challengeType]();
        }
    }
}
