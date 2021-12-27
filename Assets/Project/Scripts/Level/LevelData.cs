using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "AsteroidsGame/Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private List<AsteroidConfig> configs;

        public List<AsteroidConfig> Configs => configs;
    }

    [Serializable]
    public struct AsteroidConfig
    {
        public TupleKeyData asteroidType;

        [MinMaxSlider(1, 30)]
        public Vector2Int asteroidAmount;

        public int RandomAmount => UnityEngine.Random.Range((int)asteroidAmount.x, (int)asteroidAmount.y);
    }
}