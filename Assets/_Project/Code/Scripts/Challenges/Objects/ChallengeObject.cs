using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    public interface ChallengeObject
    {
        void Init();
        void Clean();
        void ChallengeCompleted();
    }
}
