using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

namespace AsteroidsGame.Actions
{
    [RequireComponent(typeof(Collider2D))]
    public class AsteroidCollisionListener : MonoBehaviour
    {
        [Tag]
        [SerializeField]
        private string spaceshipTag;

        [Tag]
        [SerializeField]
        private string bulletTag;
        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == spaceshipTag)
            {
                //TODO
            
            }
            else if(col.tag == bulletTag)
            {
                Destroy(col.gameObject);
                Destroy(gameObject);                
            }
            
        }
    }
}