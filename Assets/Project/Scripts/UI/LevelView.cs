using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using AsteroidsGame.Level;

namespace AsteroidsGame.UI
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI levelLabel;

        #region Unitye Methods
        protected void Awake()
        {
            LevelManager.OnLevelStarted += UpdateLevelLabel;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStarted -= UpdateLevelLabel;
        }
        #endregion

        public void UpdateLevelLabel(int visualLevel)
        {
            levelLabel.text = string.Format("Wave {0}", visualLevel);
        }
    }
}