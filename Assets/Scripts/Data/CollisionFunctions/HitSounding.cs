using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using UnityEngine;

[CreateAssetMenu(fileName = "HitSounding", menuName = "Scriptable/CollisionInteraction/HitSounding",order = 2)]
public class HitSounding : CollisionInteraction
{
    public AudioClip clip;
    public float sound;
    public override void EnterCollsion(GameObject Owner, GameObject target)
    {
        AudioSource.PlayClipAtPoint(clip,target.transform.position,sound);
    }

    public override void ExitCollsion(GameObject who, GameObject target)
    {
        
    }
}
