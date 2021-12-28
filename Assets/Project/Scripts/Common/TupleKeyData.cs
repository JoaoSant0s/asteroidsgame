using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame.Data
{
    [CreateAssetMenu(fileName = "TupleKeyData", menuName = "AsteroidsGame/Common/TupleKeyData")]
    public class TupleKeyData : ScriptableObject
    {
        public string key;
    }
}