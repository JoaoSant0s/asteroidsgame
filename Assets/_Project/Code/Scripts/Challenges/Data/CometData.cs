using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Challenges.Data
{
    [CreateAssetMenu(fileName = "CometData", menuName = "AsteroidsGame/Challenge/CometData")]
    public class CometData : ChallengeData
    {
       [Header("Configs")]
        public float speed;

        public int destroyScore;

        public int life;
    }
}