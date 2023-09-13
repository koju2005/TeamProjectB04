using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "HitSounding", menuName = "Scriptable/CollisionInteraction/HitSounding",order = 2)]
public class HitSounding : CollisionInteraction
{
    public AudioClip clip;
    public override void EnterCollsion(GameObject Owner, GameObject target)
    {
        SoundManager soundManager = GameManager.Instance._soundManager;
        soundManager.PlaySFX(clip,target.transform.position);
    }

    public override void ExitCollsion(GameObject who, GameObject target)
    {
        
    }
}
