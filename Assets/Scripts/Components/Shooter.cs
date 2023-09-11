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


    private bool isFirst = true;
    private void Start()
    {
        StartRoutine();
        isFirst = false;
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
            
        weapon.SetActive(true);
    }   
}
