using System;
using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using DefaultNamespace.Data.ItemFunction;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
[CreateAssetMenu(fileName = "ItemData",menuName = "Scriptable/ItemData",order = 2)]
public class ItemData : ScriptableObject
{
    public Sprite Image;
    public ItemFunction[] itemFunctions;
    public int[] ItemEffectValue;
}
