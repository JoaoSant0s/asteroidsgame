using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.UtilWrapper
{
    public class DestroySchedule : MonoBehaviour
    {
        [SerializeField]
        private float destroyDelayTime;
        private void Start()
        {
            Destroy(gameObject, destroyDelayTime);
        }
    }
}