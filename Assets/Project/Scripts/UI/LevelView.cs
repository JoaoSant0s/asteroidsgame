using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AsteroidsGame.UI
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI levelLabel;

        public void UpdateLevel(int visualLevel)
        {
            levelLabel.text = string.Format("Level {0}", visualLevel);
        }
    }
}