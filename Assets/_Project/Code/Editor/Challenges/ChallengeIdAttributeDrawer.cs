using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

using JoaoSant0s.CommonWrapper.GUIDrawerEditor;

using AsteroidsGame.Challenges;

namespace AsteroidsGame.ChallengesEditor
{
    [CustomPropertyDrawer(typeof(ChallengeIdAttribute))]
    public class ChallengeIdAttributeDrawer : CustomIdAttributeDrawer
    {
        protected override string[] Options => ChallengeIdAttribute.Options;
    }
}