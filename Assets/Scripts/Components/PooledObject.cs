using System;
using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace.Data
{
    public abstract class PooledObject : MonoBehaviour
    {
        public Action<GameObject> OnRelease;
        
    }
}