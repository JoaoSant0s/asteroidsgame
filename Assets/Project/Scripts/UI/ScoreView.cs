using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using AsteroidsGame.Manager;

namespace AsteroidsGame.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI scoreLabel;

        #region Unitye Methods
        protected void Awake()
        {
            ScoreManager.OnScoreUpdated += UpdateScoreLabel;
        }

        private void OnDestroy()
        {
            ScoreManager.OnScoreUpdated -= UpdateScoreLabel;
        }
        #endregion

        public void UpdateScoreLabel(int score)
        {
            scoreLabel.text = string.Format("{0}", score);
        }
    }
}