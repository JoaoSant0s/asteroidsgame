using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Spaceships.Actions;

namespace AsteroidsGame.Spaceships
{
    public class Spaceship : MonoBehaviour
    {
        public SpaceshipInvulnerableAction InvulnerableAction { get; protected set; }

        #region Unity Methods

        private void Awake()
        {
            InvulnerableAction = GetComponent<SpaceshipInvulnerableAction>();
        }

        #endregion

        #region Public Methods

        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
        }

        #endregion
    }
}