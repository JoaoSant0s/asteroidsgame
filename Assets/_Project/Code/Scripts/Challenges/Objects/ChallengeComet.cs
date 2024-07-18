using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Pool;
using JoaoSant0s.ServicePackage.Routine;

using AsteroidsGame.UtilWrapper;

namespace AsteroidsGame.Challenges.Comets
{
    public class ChallengeComet : ChallengeObject
    {
        private PoolService poolService;
        private RoutineService routineService;

        private Comet comet;

        public ChallengeComet(ChallengeManager newManager) : base(newManager) { }

        public override void Init()
        {
            poolService = Services.Get<PoolService>();
            routineService = Services.Get<RoutineService>();
            
            var target = Vector2.zero;

            comet = CreateComet();
            comet.GetComponent<CometContext>().OnDestroyed += DestroyDelay;

            comet.Init();
        }

        public override void Clean()
        {
            if (comet) comet.Dispose();
        }

        private Comet CreateComet()
        {            
            var limits = MainCanvas.Instance.Limits;

            var xValue = UnityEngine.Random.Range(-limits.x * 0.7f, limits.x * 0.7f);
            var yValue = UnityEngine.Random.Range(-limits.y * 0.7f, limits.y * 0.7f);            
            var startPosition = new Vector2(xValue, yValue);

            return poolService.Get<Comet>(startPosition, manager.transform);
        }

        private void DestroyDelay()
        {
            routineService.WaitTimeThenDo(2, ChallengeCompleted);
        }
    }
}