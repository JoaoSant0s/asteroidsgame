using System;
using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.UtilWrapper;
using UnityEngine;

namespace AsteroidsGame.Spaceships
{
    public class SpaceshipTurbine : MonoBehaviour
    {
        [Header("Components")]

        [SerializeField]
        private TrailRenderer fireTrail;

        [SerializeField]
        private MoveToOppositeSide moveToOppositiveSide;

        #region Unity Methods

        private void Start()
        {
            this.moveToOppositiveSide.OnPositionChanged += ClearFire;
        }

        private void OnDestroy()
        {
            this.moveToOppositiveSide.OnPositionChanged -= ClearFire;
        }

        #endregion

        #region Public Methods

        public void ClearFire()
        {
            this.fireTrail.Clear();
        }

        #endregion
    }
}
