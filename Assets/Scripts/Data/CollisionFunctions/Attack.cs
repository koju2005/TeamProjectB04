using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using DefaultNamespace.Data;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable/CollisionInteraction/Attack",order = 2)]
public class Attack : CollisionInteraction
{
    public override void EnterCollsion(GameObject Owner, GameObject target)
    {
        Weapon weapon = Owner.GetComponent<Weapon>();
        Health health = target.GetComponent<Health>();
        int damage = weapon.Damage; 
        if (health != null)
        {
            health.AddHealth(-damage);
        }
    }

    public override void ExitCollsion(GameObject who, GameObject target)
    {
        
    }
}
