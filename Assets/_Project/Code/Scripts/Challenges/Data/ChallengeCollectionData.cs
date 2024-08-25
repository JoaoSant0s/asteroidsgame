using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Challenges.Data
{
    [CreateAssetMenu(fileName = "ChallengeCollectionData", menuName = "AsteroidsGame/Challenge/ChallengeCollectionData")]
    public class ChallengeCollectionData : ScriptableObject
    {
        public int challengeLevelModule = 3;
        public ChallengeIdObject[] challenges;
    }

    [Serializable]
    public struct ChallengeIdObject
    {
        [ChallengeIdAttribute]
        public string id;
    }
}