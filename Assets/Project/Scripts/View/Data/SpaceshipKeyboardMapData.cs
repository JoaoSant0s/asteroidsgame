using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "SpaceshipKeyboardMapData", menuName = "AsteroidsGame/Spaceship/SpaceshipKeyboardMapData")]

    public class SpaceshipKeyboardMapData : ScriptableObject
    {
        [Header("Movements Key Code")]

        public KeyCode accelerate;
        public KeyCode rotateLeft;
        public KeyCode rotateRight;

        [Header("Actions Key Code")]

        public KeyCode shootAction;
        public KeyCode hyperSpaceAction;
    }
}