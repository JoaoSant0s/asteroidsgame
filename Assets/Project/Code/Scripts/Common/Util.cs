using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.UtilWrapper
{
    public class Util
    {
        public static Vector2 RandomDirection()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
