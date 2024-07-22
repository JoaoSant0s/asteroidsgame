using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using NUnit.Framework;
using AsteroidsGame.Asteroids.Data;

namespace AsteroidsGame.Asteroids.Tests
{
    public class AsteroidsTests
    {
        [Test]
        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 0, 0, 13)]
        [TestCase(1, 0, 1, 14)]
        [TestCase(0, 1, 1, 5)]
        [TestCase(0, 0, 1, 1)]
        public void AsteroidsEstimatedAmount_Type1_Type2_Type3_Result(int asteroidsType1, int asteroidsType2, int asteroidsType3, int result)
        {
            var asteroidSpawnerData = Resources.Load<AsteroidSpawnerData>("AsteroidSpawnerData");
            var estimatedTotal = 0;

            for (int i = 0; i < asteroidsType1; i++)
            {
                estimatedTotal += TotalAsteroidsAmount(asteroidSpawnerData, 0);
            }

            for (int i = 0; i < asteroidsType2; i++)
            {
                estimatedTotal += TotalAsteroidsAmount(asteroidSpawnerData, 1);
            }

            for (int i = 0; i < asteroidsType3; i++)
            {
                estimatedTotal += TotalAsteroidsAmount(asteroidSpawnerData, 2);
            }

            Assert.AreEqual(result, estimatedTotal, "The total values must be equals");
        }

        private int TotalAsteroidsAmount(AsteroidSpawnerData asteroidSpawnerData, int index)
        {
            var data = asteroidSpawnerData.GetAsteroidData(index);
            return asteroidSpawnerData.TotalAsteroidsAmount(data);
        }
    }
}
