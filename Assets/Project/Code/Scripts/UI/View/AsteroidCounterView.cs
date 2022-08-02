using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using JoaoSant0s.CustomVariable;

namespace AsteroidsGame.UI.View
{
    public class AsteroidCounterView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI counterLabel;

        [Header("Variables")]
        [SerializeField]
        private IntVariable totalAsteroidsVariable;

        [SerializeField]
        private IntVariable currentAsteroidsVariable;

        private int totalAsteroids;
        private int currentAsteroids;

        #region Unity Methods

        protected void Awake()
        {
            this.totalAsteroidsVariable.OnValueModified += UpdateTotalAsteroids;
            this.currentAsteroidsVariable.OnValueModified += UpdateCurrentAsteroids;
        }

        private void OnDestroy()
        {
            this.totalAsteroidsVariable.OnValueModified -= UpdateTotalAsteroids;
            this.currentAsteroidsVariable.OnValueModified -= UpdateCurrentAsteroids;
        }

        #endregion

        #region Private Methods

        private void UpdateTotalAsteroids(int previousAsteroidsTotal, int newAsteroidsTotal)
        {
            totalAsteroids = newAsteroidsTotal;
            UpdateView();
        }

        private void UpdateCurrentAsteroids(int previousAsteroidsAmount, int currentAsteroidsAmount)
        {
            currentAsteroids = currentAsteroidsAmount;
            UpdateView();
        }

        private void UpdateView()
        {
            counterLabel.text = string.Format("{0}/{1}", totalAsteroids - currentAsteroids, totalAsteroids);
        }

        #endregion
    }
}