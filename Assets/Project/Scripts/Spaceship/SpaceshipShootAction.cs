using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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

#region Unity Methods

        private void Awake()
        {
            InputController.ShootAction += Shoot;
        }

        private void OnDestroy()
        {
            InputController.ShootAction -= Shoot;
        }

#endregion

        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
            var movementAction = bullet.GetComponent<BulletMovementAction>();
            
            movementAction.Move(transform.up);
        }
    }
}