using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Pool;

using AsteroidsGame.UI;
using AsteroidsGame.Unit;

namespace AsteroidsGame.Actions
{
    public class SpaceshipShootAction : MonoBehaviour
    {
        [SerializeField]
        private Bullet bulletPrefab;

        [SerializeField]
        private Transform bulletOrigin;

        private PoolService poolService;

#region Unity Methods

        private void Awake()
        {
            InputController.ShootAction += Shoot;
        }

        private void Start() 
        {
            poolService = Services.Get<PoolService>();
        }

        private void OnDestroy()
        {
            InputController.ShootAction -= Shoot;
        }

#endregion

        private void Shoot()
        {
            var bullet = InstatiateBullet();
            var movementAction = bullet.GetComponent<BulletMovementAction>();
            
            movementAction.Move(transform.up);
        }

        private Bullet InstatiateBullet()
        {

            var bullet = poolService.Get<Bullet>(null, bulletOrigin.position, Quaternion.identity);
            //var bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
            return bullet;
        }
    }
}