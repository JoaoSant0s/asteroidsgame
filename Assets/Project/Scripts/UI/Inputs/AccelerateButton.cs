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
        public static event Action<float> AcceleratingSpaceShip;
        public static event Action StopAccelerateSpaceShip;

        private ButtonHold buttonAccelerate;

        #region Unity Methods

        private void Awake()
        {
            this.buttonAccelerate = GetComponent<ButtonHold>();
        }

        private void Start()
        {
            this.buttonAccelerate.OnHoldEvent.AddListener(() =>
            {
                AcceleratingSpaceShip?.Invoke(1);
            });

            this.buttonAccelerate.OnUpEvent.AddListener(() =>
            {
                StopAccelerateSpaceShip?.Invoke();
            });
        }

        #endregion
    }
}
