using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Common;
using DefaultNamespace.Data;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    
    private PrefabsPoolManager _prefabsPoolManager;
    public string[] AttackPrefabsName;
    public int[] AttackDelay;

    private Transform playerTransform;
    private Animator anim;

    private bool isFirst = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartRoutine();
        isFirst = false;
        playerTransform = GameManager.Instance.GetPlayer().transform;
       
    }

    private void OnEnable()
    {
        if (!isFirst)
        {
            StartRoutine();
        }
    }

    public void StartRoutine()
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

    IEnumerator ShootCoroutine(int i)
    {
        WaitForSeconds seconds = CoroutineTime.GetWaitForSeconds(AttackDelay[i]);
        while (true)
        {
            Shoot(i);
            yield return seconds;
        }
    }

    private void Shoot(int i)
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
        if(anim)
            anim.SetTrigger("Attack");
    }
}
