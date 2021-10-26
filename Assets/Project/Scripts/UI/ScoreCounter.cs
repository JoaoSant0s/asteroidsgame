using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace AsteroidsGame.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI scoreLabel;

        public void UpdateScore(string text)
        {
            scoreLabel.text = text;
        }
    }
}