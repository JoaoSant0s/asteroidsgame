using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using JoaoSant0s.CustomVariable;

namespace AsteroidsGame.UI.View
{
    public class LevelView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private TextMeshProUGUI levelLabel;

        [Header("Variables")]
        [SerializeField]
        private IntVariable globalLevelVariable;

        #region Unitye Methods

        protected void Awake()
        {
            this.globalLevelVariable.AddChangeListener(UpdateLevelLabel);
        }

        private void OnDestroy()
        {
            this.globalLevelVariable.RemoveChangeListener(UpdateLevelLabel);
        }

        #endregion

        public void UpdateLevelLabel(int previousLevel, int newLevel)
        {
            levelLabel.text = string.Format("Wave {0}", newLevel + 1);
        }
    }
}