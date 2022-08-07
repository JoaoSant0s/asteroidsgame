using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Ads.Data
{
    [CreateAssetMenu(fileName = "AdConfig", menuName = "AsteroidsGame/Ad/AdConfig")]
    public class AdConfigData : ScriptableObject
    {
        [Header("General")]
        public bool testMode;

        [Header("Android")]
        public string playStoreGameId;
        public string placementRewardedVideoId;
    }
}
