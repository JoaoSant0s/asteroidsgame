using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "LevelCollectionData", menuName = "AsteroidsGame/LevelCollectionData")]
    public class LevelCollectionData : ScriptableObject
    {        
        public float nextLevelDelay;
        public List<LevelData> levels;
    }
}