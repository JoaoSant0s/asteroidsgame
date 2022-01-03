using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace AsteroidsGame.UI
{
    public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private UnityEvent downEvent;

        [SerializeField]
        private UnityEvent holdEvent;

        [SerializeField]
        private UnityEvent upEvent;

        private bool isPressed;

        public UnityEvent HoldEvent => holdEvent;

        #region Unity Methods        

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;
            downEvent?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;
            upEvent?.Invoke();
        }

        private void Update()
        {
            OnUpdateSelected();
        }

        #endregion

        #region UI Methods

        public void SetNotPressed()
        {
            Reset();
        }

        #endregion


        #region Private Methods

        private void Reset()
        {
            isPressed = false;
            upEvent?.Invoke();
        }

        private void OnUpdateSelected()
        {
            if (!isPressed) return;

            holdEvent?.Invoke();
        }

        #endregion

    }
}
