using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Pool;

using AsteroidsGame.UI;
using AsteroidsGame.Unit;
using AsteroidsGame.Manager;

namespace AsteroidsGame.Actions
{
    public class SpaceshipShootAction : MonoBehaviour
    {
        [SerializeField]
        private Transform bulletOrigin;

        private PoolService poolService;

        #region Unity Methods

        private void Awake()
        {
            InputController.ShootAction += Shoot;
#if UNITY_EDITOR
            InputEditorController.ShootAction += Shoot;
#endif
        }

        private void Start()
        {
            poolService = Services.Get<PoolService>();
        }

        private void OnDestroy()
        {
            InputController.ShootAction -= Shoot;
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
            var parent = SpaceshipSpawner.Instance.BulletsArea;
            var bullet = poolService.Get<Bullet>(bulletOrigin.position, Quaternion.identity, parent);
            return bullet;
        }
    }
}