using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Data.ItemFunction;
using UnityEngine;

public class Effector : MonoBehaviour
{
    public ItemFunction[] effects;
    public int[] ItemEffectValue;
    public void ApplyEffect(GameObject player)
    {
        for (int i = 0; i < effects.Length; ++i)
        {
            effects[i].Apply(player,ItemEffectValue[i]);
        }
    }
}
