using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CustomVariable;

using AsteroidsGame.Asteroid;

namespace AsteroidsGame.CustomVariable
{
    [CreateAssetMenu(fileName = "AsteroidContextVariable", menuName = "AsteroidsGame/CustomVariables/AsteroidContextVariable")]
    public class AsteroidContextVariable : Variable<AsteroidContext>
    {
        #region Protected Override Methods

        protected override void OnModify(AsteroidContext newValue)
        {
            Value = newValue;
        }

        #endregion
    }
}
