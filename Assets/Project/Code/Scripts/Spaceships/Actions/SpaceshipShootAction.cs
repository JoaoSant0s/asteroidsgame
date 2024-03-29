using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Pool;

using AsteroidsGame.UI;
using AsteroidsGame.Bullets;
using AsteroidsGame.UI.Inputs;

namespace AsteroidsGame.Spaceships.Actions
{
    public class SpaceshipShootAction : MonoBehaviour
    {
        public delegate Transform OnGetBulletArea();
        public static event OnGetBulletArea GetBulletArea;

        [SerializeField]
        private Transform bulletOrigin;

        private PoolService poolService;

        private Transform bulletsArea;

        #region Unity Methods

        private void Awake()
        {
            ShootButton.ShootAction += Shoot;
#if UNITY_EDITOR
            InputEditorController.ShootAction += Shoot;
#endif
        }

        private void Start()
        {
            poolService = Services.Get<PoolService>();
            bulletsArea = GetBulletArea?.Invoke();
        }

        private void OnDestroy()
        {
            ShootButton.ShootAction -= Shoot;
#if UNITY_EDITOR
            InputEditorController.ShootAction -= Shoot;
#endif
        }

        #endregion

        private void Shoot()
        {
            var bullet = InstatiateBullet();
            bullet.Move(transform.up);
        }

        private Bullet InstatiateBullet()
        {
            var bullet = poolService.Get<Bullet>(bulletOrigin.position, Quaternion.identity, bulletsArea);
            return bullet;
        }
    }
}