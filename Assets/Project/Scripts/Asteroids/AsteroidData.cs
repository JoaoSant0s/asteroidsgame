using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "AsteroidData", menuName = "AsteroidsGame/AsteroidData")]
    public class AsteroidData : ScriptableObject
    {
        public float speed;
    }
}