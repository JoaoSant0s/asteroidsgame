using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "SaveConfig", menuName = "AsteroidsGame/Save/SaveConfig")]
    public class SaveConfig : ScriptableObject
    {
        [Header("Keys")]
        public string playerSaveKey;
        public string levelSaveKey;

        [Header("Data")]
        public SpaceshipSpawnerData spaceshipSpawnerData;
    }
}