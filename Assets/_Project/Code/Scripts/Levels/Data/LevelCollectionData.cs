using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.Levels.Data
{
    [CreateAssetMenu(fileName = "LevelCollectionData", menuName = "AsteroidsGame/Level/LevelCollectionData")]
    public class LevelCollectionData : ScriptableObject
    {
        [Header("Config")]
        public int levelToReturnAfterGameOver;
        public float nextLevelDelay;
        public List<LevelData> levels;
    }
}