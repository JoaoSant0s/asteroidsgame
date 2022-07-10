using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using AsteroidsGame.Manager;

namespace AsteroidsGame.UI
{
    public class LifeBarView : MonoBehaviour
    {
        [SerializeField]
        private Image lifeIcon;

        #region Unity Methods

        private void Awake()
        {
            SpaceshipSpawner.UpdateSpaceshipLife += RefreshLifeIcons;
        }

        private void OnDestroy()
        {
            SpaceshipSpawner.UpdateSpaceshipLife -= RefreshLifeIcons;
        }

        #endregion

        #region Private Methods

        private void RefreshLifeIcons(int previousLife, int newLife)
        {
            CleanLifeElements();

            CreateLifeElements(newLife);
        }

        private void CleanLifeElements()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void CreateLifeElements(int newLife)
        {
            for (int i = 0; i < newLife; i++)
            {
                Instantiate(lifeIcon, transform);
            }
        }

        #endregion
    }
}