using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Pool;
using System.Threading.Tasks;

namespace AsteroidsGame.UtilWrapper
{
    public class DisposeSchedule : PoolBehaviour
    {
        [SerializeField]
        private int destroyDelayTimeInMiliseconds;

        protected async override void OnShow()
        {
            await Task.Delay(destroyDelayTimeInMiliseconds);
            Dispose();
        }
    }
}
