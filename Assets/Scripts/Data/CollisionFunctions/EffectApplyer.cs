using System;
using UnityEngine;

namespace Data.Functions
{
    [CreateAssetMenu(fileName = "EffectApplyer", menuName = "Scriptable/CollisionInteraction/EffectApplyer",order = 2)]
    public class EffectApplyer : CollisionInteraction
    {
        public override void EnterCollsion(GameObject Owner, GameObject target)
        {
            if(target.CompareTag("Player"))
                Owner.GetComponent<Effector>().ApplyEffect(target);
        }

        public override void ExitCollsion(GameObject who, GameObject target)
        {
            
        }
    }
}