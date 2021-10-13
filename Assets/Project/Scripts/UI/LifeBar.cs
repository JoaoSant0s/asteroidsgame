using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using AsteroidsGame.Manager;

namespace AsteroidsGame.UI
{
    public class LifeBar : MonoBehaviour
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

        private void RefreshLifeIcons(int value)
        {
            foreach (Transform child in transform) 
            {
                Destroy(child.gameObject);
            }
            
            for (int i = 0; i < value; i++)
            {
                Instantiate(lifeIcon, transform);
            }
        }
    }
}