using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Challenges.Data;
using AsteroidsGame.Challenges.Comets;

namespace AsteroidsGame.Challenges
{
    public class ChallengeFactory
    {
        public delegate ChallengeObject OnCreateChallenge();

        private Dictionary<string, OnCreateChallenge> challengeFactory;

        public ChallengeFactory(ChallengeManager manager)
        {
            challengeFactory = new();

            for (int i = 0; i < ChallengeIdAttribute.Options.Length; i++)
            {
                var optionKey = ChallengeIdAttribute.Options[i];
                challengeFactory.Add(optionKey, () => { return new ChallengeComet(manager); });
            }
        }

        public ChallengeObject CreateChallenge(ChallengeIdObject challenge)
        {
            Debug.Assert(challengeFactory.ContainsKey(challenge.id), "To create this Challenge you must add this new Challenge Class to the ChallengeIdAttribute Property Class");
            return challengeFactory[challenge.id]();
        }
    }
}
