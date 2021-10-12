using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "SpaceshipData", menuName = "AsteroidsGame/SpaceshipData")]
    public class SpaceshipData : ScriptableObject
    {
        public float rotateSpeed;        
        public float forwardForce;
        public float maxForwardVelocity;

    }
}