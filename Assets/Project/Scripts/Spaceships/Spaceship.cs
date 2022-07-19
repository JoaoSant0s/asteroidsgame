using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Spaceships.Actions;

namespace AsteroidsGame.Spaceships
{
    public class Spaceship : MonoBehaviour
    {
        private SpaceshipInvulnerableAction invulnerableAction;

        public SpaceshipInvulnerableAction InvulnerableAction
        {
            get
            {
                if (invulnerableAction == null)
                {
                    invulnerableAction = GetComponent<SpaceshipInvulnerableAction>();
                }
                return invulnerableAction;
            }
        }
    }
}