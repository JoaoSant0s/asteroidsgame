using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsGame.Challenges.Comets;

namespace AsteroidsGame.Challenges
{
    public class ChallengeIdAttribute : PropertyAttribute
    {
        private static string[] options = new string[] { nameof(ChallengeComet) };

        public static string[] Options => options;
    }
}