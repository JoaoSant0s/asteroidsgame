using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.Spaceships.Actions
{
    public class SpaceshipInvulnerableAction : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipContext context;

        [SerializeField]
        private SpaceshipShield spaceshipShield;

        private Coroutine disableRoutine;

        #region Public Methods

        public void RunDefaultInvulnerability()
        {
            if (context.IsInvulnerable) return;
            context.IsInvulnerable = true;

            spaceshipShield.StartAnimation(context.Data.InvulnarableConfig());
            StopInvulnerability();
        }

        public void RunInfinityInvulnerability()
        {
            if (context.IsInvulnerable)
            {
                StopDisableRoutine();
                return;
            }

            context.IsInvulnerable = true;
            spaceshipShield.StartAnimation(context.Data.InvulnarableConfig());
        }

        public void StopInvulnerability()
        {
            disableRoutine = StartCoroutine(DisableInvulnerabilityRoutine(context.Data.invulnerabilityDuration));
        }

        #endregion

        #region Private Methods

        private void StopDisableRoutine()
        {
            if (disableRoutine != null)
            {
                StopCoroutine(disableRoutine);
                disableRoutine = null;
            }
        }

        private IEnumerator DisableInvulnerabilityRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            spaceshipShield.StopAnimation();
            context.IsInvulnerable = false;
        }

        #endregion
    }
}