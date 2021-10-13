using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Main.Scene;
using AsteroidsGame.Unit;

namespace AsteroidsGame.Manager
{
    public class SpaceshipSpawner : SceneComponent
    {
        [SerializeField]
        private Spaceship spaceshipPrefab;
        
        public override void Initialize()
        {
            Instantiate(spaceshipPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}