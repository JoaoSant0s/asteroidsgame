using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using AsteroidsGame.Manager;

namespace AsteroidsGame.UI
{
    public class AsteroidCounterView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI counterLabel;

        private int totalAsteroids;
        private int currentAsteroids;

        #region Unity Methods

        private void Awake()
        {
            AsteroidSpawner.TotalAsteroids += UpdateTotalAsteroids;
            AsteroidSpawner.CurrentAsteroids += UpdateCurrentAsteroids;
        }

        private void OnDestroy()
        {
            AsteroidSpawner.TotalAsteroids -= UpdateTotalAsteroids;
            AsteroidSpawner.CurrentAsteroids += UpdateCurrentAsteroids;
        }

        #endregion

        #region Private Methods

        private void UpdateTotalAsteroids(int total)
        {
            totalAsteroids = total;

            UpdateView();
        }

        private void UpdateCurrentAsteroids(int current)
        {
            currentAsteroids = current;
            UpdateView();
        }

        private void UpdateView()
        {
            counterLabel.text = string.Format("{0}/{1}", totalAsteroids - currentAsteroids, totalAsteroids);
        }

        #endregion
    }
}