using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsGame.UI.Inputs
{
    [RequireComponent(typeof(Button))]
    public class ShootButton : MonoBehaviour
    {
        public static event Action ShootAction;

        private Button buttonShoot;

        #region Unity Methods

        private void Awake()
        {
            buttonShoot = GetComponent<Button>();
        }

        private void Start()
        {
            buttonShoot.onClick.AddListener(() =>
            {
                ShootAction?.Invoke();
            });
        }

        #endregion
    }
}
