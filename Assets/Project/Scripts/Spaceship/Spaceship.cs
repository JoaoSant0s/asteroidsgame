using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame.Unit
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipContext context;
        public bool IsInvulnerable { get; private set; }

        #region Public Methods

        public void RunDefaultInvulnerability()
        {
            if (IsInvulnerable) return;
            IsInvulnerable = true;
            StartCoroutine(DisableInvulnerabilityRoutine(context.Data.invulnerabilityDuration));
        }

        #endregion

        #region Private Methods

        private IEnumerator DisableInvulnerabilityRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            IsInvulnerable = false;
        }

        #endregion
    }
}