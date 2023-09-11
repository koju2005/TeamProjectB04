using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemManager : ObjectPoolManager<ItemData>
{
    protected override void MakeObject(GameObject obj, ItemData data)
    {
        SpriteRenderer spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        Effector effector = obj.GetComponent<Effector>();

        spriteRenderer.sprite = data.Image;
        effector.effects = data.itemFunctions;
    }
}
