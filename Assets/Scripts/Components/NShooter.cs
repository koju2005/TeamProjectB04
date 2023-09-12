using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Common;
using UnityEngine;
using Random = System.Random;

public class NShooter : Shooter
{
    public int RepeatCount=3;
    public float RepeatTime=0.5f;

    protected IEnumerator ShootCoroutine()
    {
        WaitForSeconds seconds = CoroutineTime.GetWaitForSeconds(AttackDelay[0]);
        WaitForSeconds repeat = CoroutineTime.GetWaitForSeconds(RepeatTime);
        Random random = new Random();
        while (true)
        {
            int select = random.Next(0, AttackPrefabsName.Length);
            for (int j = 0; j <RepeatCount;++j)
            {
                Shoot(select);
                yield return repeat;
            }
            yield return seconds;
        }
    }
    
    public override void StartShootOperation()
    {
        _prefabsPoolManager = GameManager.Instance._prefabsPoolManager;
        StartCoroutine(ShootCoroutine());
    }
}
