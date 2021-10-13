using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Actions;
using AsteroidsGame.Data;
using AsteroidsGame.Unit;

using TMPro;
using AsteroidsGame.UI.Popup;

namespace AsteroidsGame.UI
{
    public class ScoreCounter : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI scoreLabel;

        private int scorePoints;

#region Unitye Methods
       protected void Awake() 
        {                        
            AsteroidCollisionListener.BulletCollideAsteroid += BulletshipCollideAsteroid;
            GameOverScreenPopup.RestartGame += RestartScore;
        }

        private void OnDestroy() 
        {
            AsteroidCollisionListener.BulletCollideAsteroid -= BulletshipCollideAsteroid;
            GameOverScreenPopup.RestartGame -= RestartScore;
        }

#endregion

        private void BulletshipCollideAsteroid(Asteroid asteroid, AsteroidData data)
        {
            scorePoints += data.destroyScore;

            scoreLabel.text = string.Format("{0}", scorePoints);
        }

        private void RestartScore()
        {
            scorePoints = 0;
            
            scoreLabel.text = string.Format("{0}", scorePoints);
        }
    }
}