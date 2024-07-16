using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Challenges.Data
{
    [CreateAssetMenu(fileName = "ChallengeCollectionData", menuName = "AsteroidsGame/Challenge/ChallengeCollectionData")]
    public class ChallengeCollectionData : ScriptableObject
    {
        [SerializeField]
        private int levelIndexModel = 3;

        [SerializeField]
        private ChallengeData[] challengeDatas;
        
    }
}