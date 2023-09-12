using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Common;
using DefaultNamespace.Data;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    
    protected PrefabsPoolManager _prefabsPoolManager;
    public string[] AttackPrefabsName;
    public float[] AttackDelay;

    private Transform playerTransform;
    private Animator anim;

    private bool isFirst = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartShootOperation();
        isFirst = false;
        playerTransform = GameManager.Instance.GetPlayer().transform;
       
    }

    private void OnEnable()
    {
        if (!isFirst)
        {
            StartShootOperation();
        }
    }

    public virtual void StartShootOperation()
    {
        _prefabsPoolManager = GameManager.Instance._prefabsPoolManager;
        for (int i=0;i<AttackPrefabsName.Length;++i)
        {
            StartCoroutine(ShootCoroutine(i));
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected virtual IEnumerator ShootCoroutine(int i)
    {
        WaitForSeconds seconds = CoroutineTime.GetWaitForSeconds(AttackDelay[i]);
        while (true)
        {
            Shoot(i);
            yield return seconds;
        }
    }

    protected void Shoot(int i)
    {
        GameObject weapon = _prefabsPoolManager.Get(AttackPrefabsName[i]);
        weapon.transform.position = transform.position;
        GameObject player = GameManager.Instance.GetPlayer();
        AutoMover mover = weapon.GetComponent<AutoMover>();
        mover.accelate = (player.transform.position - weapon.transform.position).normalized * 5;
        
        PlayShotAnim();
        weapon.SetActive(true);
    }

    private void PlayShotAnim()
    {
        if(anim && anim.runtimeAnimatorController)
            anim.SetTrigger("Attack");
    }
}
