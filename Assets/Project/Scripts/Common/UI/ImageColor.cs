using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsGame.UI
{
    [RequireComponent(typeof(Image))]
    public class ImageColor : MonoBehaviour
    {
        [Header("Colors")]

        [SerializeField]
        private Color mainColor;

        [SerializeField]
        private Color alternativeColor;
        
        private Image image;

        private void Awake() 
        {
            image = GetComponent<Image>();
        }

        public void SetMainColor()
        {
            image.color = mainColor;
        }

        public void SetAlternativeColor()
        {
            image.color = alternativeColor;
        }
    }
}