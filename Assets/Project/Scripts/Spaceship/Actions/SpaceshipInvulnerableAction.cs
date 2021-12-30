using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Unit;

namespace AsteroidsGame.Actions
{
    public class SpaceshipInvulnerableAction : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipContext context;

        [SerializeField]
        private SpaceshipShield spaceshipShield;

        #region Public Methods

        public void RunDefaultInvulnerability()
        {
            if (context.IsInvulnerable) return;
            context.IsInvulnerable = true;

            spaceshipShield.StartAnimation(context.Data.InvulnarableConfig());
            StartCoroutine(DisableInvulnerabilityRoutine(context.Data.invulnerabilityDuration));
        }

        #endregion

        #region Private Methods

        private IEnumerator DisableInvulnerabilityRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            context.IsInvulnerable = false;
            spaceshipShield.StopAnimation();
        }

        #endregion
    }
}