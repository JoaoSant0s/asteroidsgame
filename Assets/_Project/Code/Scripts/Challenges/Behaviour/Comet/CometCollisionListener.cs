using System.Collections;
using System.Collections.Generic;
using AsteroidsGame.Bullets;
using NaughtyAttributes;
using UnityEngine;

namespace AsteroidsGame.Challenges.Comets
{
    [RequireComponent(typeof(Collider2D))]
    public class CometCollisionListener : MonoBehaviour
    {
        [Tag]
        [SerializeField]
        private string bulletTag;

        [SerializeField]
        private CometContext context;

         #region Unity Methods
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(bulletTag)) return;
            
            col.GetComponent<Bullet>()?.Dispose();

            context.Damaged();
        }

        #endregion     
    }
}