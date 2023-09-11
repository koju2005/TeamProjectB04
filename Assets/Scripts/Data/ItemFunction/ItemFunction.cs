using UnityEngine;

namespace DefaultNamespace.Data.ItemFunction
{
    public abstract class ItemFunction : ScriptableObject
    {
        public abstract void Apply(GameObject Player, int effectValue);
    }
}