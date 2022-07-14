using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using JoaoSant0s.CustomVariable;

using AsteroidsGame.Level;

namespace AsteroidsGame.UI
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
            this.globalLevelVariable.OnValueModified += UpdateLevelLabel;
        }

        private void OnDestroy()
        {
            this.globalLevelVariable.OnValueModified -= UpdateLevelLabel;
        }
        #endregion

        public void UpdateLevelLabel(int previousLevel, int newLevel)
        {
            levelLabel.text = string.Format("Wave {0}", newLevel + 1);
        }
    }
}