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
        if (LayerMask.NameToLayer(weapon.owner) != target.layer)
        {
            Health health = target.GetComponent<Health>();
            Animator animator = target.GetComponent<Animator>();
            if(animator)
                animator.SetTrigger("Hit");
            int damage = weapon.Damage; 
            if (health != null)
            {
                health.AddHealth(-damage);
            }
        }
    }

    public override void ExitCollsion(GameObject who, GameObject target)
    {
        
    }
}
