using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "UINavigationKeyboardMapData", menuName = "AsteroidsGame/Player/UINavigationKeyboardMapData")]
    public class UINavigationKeyboardMapData : ScriptableObject
    {
        public KeyCode confirmAction;
    }
}