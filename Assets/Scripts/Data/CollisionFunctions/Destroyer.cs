using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using DefaultNamespace.Data;
using UnityEngine;
[CreateAssetMenu(fileName = "Destroyer", menuName = "Scriptable/CollisionInteraction/Destroy",order = 2)]
public class Destroyer : CollisionInteraction
{
    public override void EnterCollsion(GameObject Owner,GameObject target)
    {
        if (target.activeSelf)
        {
            PooledObject pool = target.GetComponent<PooledObject>();
            if (pool.OnRelease != null)
                pool.OnRelease(target);
            else
                Destroy(target);            
        }
    }

    public override void ExitCollsion(GameObject who, GameObject target)
    {
        
    }
}
