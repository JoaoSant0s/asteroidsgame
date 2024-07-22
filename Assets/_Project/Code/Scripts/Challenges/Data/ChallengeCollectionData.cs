using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Challenges.Data
{
    public enum ChallengeType
    {
        Comet
    }

    [CreateAssetMenu(fileName = "ChallengeCollectionData", menuName = "AsteroidsGame/Challenge/ChallengeCollectionData")]
    public class ChallengeCollectionData : ScriptableObject
    {
        public int challengeLevelModule = 3;
        public ChallengeType[] challengeTypes;
    }
}