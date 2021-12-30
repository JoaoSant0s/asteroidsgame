using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace AsteroidsGame.Editor
{
    public class EditorTools
    {
#if UNITY_EDITOR

        [MenuItem("Tools/Clean Player Prefas", false, 1)]
        static void CleanPlayerPrefas()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Player Prefs Cleaned");
        }
#endif

    }
}