using System;
using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.UtilWrapper;
using UnityEngine;

namespace AsteroidsGame.Spaceships
{
    public class SpaceshipTurbine : MonoBehaviour
    {
        [Header("Behaviours")]

        [SerializeField]
        private GameObject turbineLeftBehaviour;

        [SerializeField]
        private GameObject turbineRightBehaviour;

        #region Public Methods

        public void EnableFire(bool enable)
        {
            this.turbineLeftBehaviour.SetActive(enable);
            this.turbineRightBehaviour.SetActive(enable);
        }

        #endregion
    }
}
