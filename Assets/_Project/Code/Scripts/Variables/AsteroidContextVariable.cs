using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CustomVariable;

using AsteroidsGame.Asteroids;

namespace AsteroidsGame.CustomVariable
{
    [CreateAssetMenu(fileName = "AsteroidContextVariable", menuName = "AsteroidsGame/CustomVariables/AsteroidContextVariable")]
    public class AsteroidContextVariable : Variable<AsteroidContext> { }
}
