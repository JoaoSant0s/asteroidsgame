using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Challenges.Data
{
    [CreateAssetMenu(fileName = "ChallengeData", menuName = "AsteroidsGame/Challenge/ChallengeData")]
    public class ChallengeData : ScriptableObject
    {
        [Header("Configs")]
        public float speed;

        public int destroyScore;

        public int life;
    }
}
