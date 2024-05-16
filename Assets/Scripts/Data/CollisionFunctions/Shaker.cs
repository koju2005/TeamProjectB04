using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Data.Functions;
using DefaultNamespace;
using DefaultNamespace.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "Shaker", menuName = "Scriptable/CollisionInteraction/Shaker",order = 2)]
public class Shaker : CollisionInteraction
{
    public override void EnterCollsion(GameObject Owner, GameObject target)
    {
        if (target.TryGetComponent(out Health health))
        {
            SpriteRenderer renderer = target.GetComponentInChildren<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = Color.red;
                health.HitRecoveryColor(ColorReturn, target);
            }
        }
    }

    public override void ExitCollsion(GameObject who, GameObject target)
    {
       
    }

    private IEnumerator ColorReturn(GameObject target)
    {
        yield return CoroutineTime.GetWaitForSeconds(0.5f);
        SpriteRenderer renderer = target.GetComponentInChildren<SpriteRenderer>();
        renderer.color = Color.white;
    }
}
