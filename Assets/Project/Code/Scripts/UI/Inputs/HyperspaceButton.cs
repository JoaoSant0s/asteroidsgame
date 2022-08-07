using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsGame.UI.Inputs
{
    [RequireComponent(typeof(Button))]
    public class HyperspaceButton : MonoBehaviour
    {
        public static event Action HyperSpaceAction;

        private Button buttonHyperSpace;

        #region Unity Methods

        private void Awake()
        {
            buttonHyperSpace = GetComponent<Button>();
        }

        private void Start()
        {
            buttonHyperSpace.onClick.AddListener(() =>
            {
                HyperSpaceAction?.Invoke();
            });
        }

        #endregion
    }
}
