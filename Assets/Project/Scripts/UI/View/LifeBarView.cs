using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.CustomVariable;

namespace AsteroidsGame.UI.View
{
    public class LifeBarView : MonoBehaviour
    {
        [SerializeField]
        private Image lifeIcon;

        [Header("Variables")]
        [SerializeField]
        private IntVariable lifeVariable;

        #region Unity Methods

        protected void Awake()
        {
            this.lifeVariable.OnValueModified += RefreshLifeIcons;
        }

        private void OnDestroy()
        {
            this.lifeVariable.OnValueModified -= RefreshLifeIcons;
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