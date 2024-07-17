using System.Collections;
using System.Collections.Generic;
using JoaoSant0s.CommonWrapper;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    public class ChallengeComet : ChallengeObject
    {
        public override void Init()
        {
            Debugs.Log("ChallengeComet", "Init");
        }

        public override void Clean()
        {
            Debugs.Log("ChallengeComet", "Clean");
        }
    }
}