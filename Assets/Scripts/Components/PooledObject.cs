using System;
using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace.Data
{
    public class PooledObject : MonoBehaviour
    {
        public Action<GameObject> OnRelease;
        
    }
}