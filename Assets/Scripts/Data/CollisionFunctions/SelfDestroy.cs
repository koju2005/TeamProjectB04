using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using DefaultNamespace.Data;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "SelfDestroy", menuName = "Scriptable/CollisionInteraction/SelfDestroy",order = 2)]
public class SelfDestroy : CollisionInteraction
{
    public override void EnterCollsion(GameObject Owner, GameObject target)
    {
        PooledObject pool = Owner.GetComponent<PooledObject>();
        if (target.activeSelf)
        {
            if (pool.OnRelease != null)
                pool.OnRelease.Invoke(Owner);
            else
                Destroy(Owner);
        }
    }

    public override void ExitCollsion(GameObject who, GameObject target)
    {
        
    }
}
