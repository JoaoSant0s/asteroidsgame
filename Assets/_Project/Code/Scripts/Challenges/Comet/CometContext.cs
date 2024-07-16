using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Challenges.Data;
using UnityEngine;

namespace AsteroidsGame.Challenges
{
    public class CometContext : MonoBehaviour
    {
        [SerializeField]
        private ChallengeData data;

        public ChallengeData Data => data;
    }
}