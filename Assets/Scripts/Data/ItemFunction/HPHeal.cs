using UnityEngine;

namespace DefaultNamespace.Data.ItemFunction
{
    [CreateAssetMenu(fileName = "HPHeal", menuName = "Scriptable/ItemInteraction/HPHeal",order = 2)]
    public class HPHeal : ItemFunction
    {
        public override void Apply(GameObject Player, int effectValue)
        { 
            Health health = Player.GetComponent<Health>();
            if (health != null)
            {
                health.AddHealth(effectValue);
            }
               
        }
    }
}