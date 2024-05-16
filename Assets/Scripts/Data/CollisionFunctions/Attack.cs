using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using DefaultNamespace.Data;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable/CollisionInteraction/Attack",order = 2)]
public class Attack : CollisionInteraction
{
    // 충돌이 발생했을 때
    public override void EnterCollsion(GameObject Owner, GameObject target)
    {
        if (Owner.TryGetComponent(out Weapon weapon) && LayerMask.NameToLayer(weapon.owner) != target.layer)
        {
            if (target.TryGetComponent(out Health health))
                health.AddHealth(weapon.Damage);
        }
    }
    // 충돌에서 벗어났을 때
    public override void ExitCollsion(GameObject who, GameObject target)
    {
        
    }
    
    
#if UNITY_EDITOR
    public override void AddRequireComponent(GameObject Owner)
    {
        if (!Owner.TryGetComponent(out Weapon weapon))
        {
            Owner.AddComponent<Weapon>();
        }
    }

    public override Component GetRemoveComponent(GameObject Owner)
    {
        if (Owner.TryGetComponent(out Weapon weapon))
        {
            return weapon;
        }

        return null;
    }
#endif
}
