using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "SaveConfig", menuName = "AsteroidsGame/Save/SaveConfig")]
    public class SaveConfig : ScriptableObject
    {
        public string playerSaveKey;
    }
}