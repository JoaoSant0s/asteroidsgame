using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using JoaoSant0s.CustomVariable;

using AsteroidsGame.Manager;

namespace AsteroidsGame.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI scoreLabel;

        [Header("Variables")]
        [SerializeField]
        private IntVariable scoreVariable;

        #region Unitye Methods
        protected void Awake()
        {
            this.scoreVariable.OnValueModified += UpdateScoreLabel;
        }

        private void OnDestroy()
        {
            this.scoreVariable.OnValueModified -= UpdateScoreLabel;
        }
        #endregion

        public void UpdateScoreLabel(int previousScore, int newScore)
        {
            scoreLabel.text = string.Format("{0}", newScore);
        }
    }
}