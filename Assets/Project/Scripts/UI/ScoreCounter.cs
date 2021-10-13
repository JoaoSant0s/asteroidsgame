using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AsteroidsGame.Actions;
using AsteroidsGame.Data;
using AsteroidsGame.Unit;

using TMPro;

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
        }

        private void OnDestroy() 
        {
            AsteroidCollisionListener.BulletCollideAsteroid -= BulletshipCollideAsteroid;
        }

#endregion

        private void BulletshipCollideAsteroid(Asteroid asteroid, AsteroidData data)
        {
            scorePoints += data.destroyScore;

            scoreLabel.text = string.Format("{0}", scorePoints);
        }
    }
}