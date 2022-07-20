using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper;

namespace AsteroidsGame.UI.Inputs
{
    [RequireComponent(typeof(ButtonHold))]
    public class AccelerateButton : MonoBehaviour
    {
        public static event Action<float> AccelerateSpaceShip;

        private ButtonHold buttonAccelerate;

        #region Unity Methods

        private void Awake()
        {
            buttonAccelerate = GetComponent<ButtonHold>();
        }

        private void Start()
        {
            buttonAccelerate.onHoldEvent.AddListener(() =>
            {
                AccelerateSpaceShip?.Invoke(1);
            });
        }

        #endregion
    }
}
