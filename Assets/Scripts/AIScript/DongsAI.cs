using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using DefaultNamespace;
using DefaultNamespace.Common;
using TMPro;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DongsAI : MonoBehaviour
{
    [SerializeField]private Vector2 spawnRange;
    private Health _health;
    private PrefabsPoolManager _prefabsManager;
    private DialogTyper _dialogTyper;
    
    private bool NextOperation;
    private string currentCoroutineString;
    private Coroutine currentPhase;

    private Vector3[] spawnPos = new Vector3 [8];
    private GameObject[] isMonsterDie = new GameObject[8];
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _dialogTyper = GetComponentInChildren<DialogTyper>();
        _health.OnHealthChanged += Interrupt;
    }

    private void Start()
    {
        _prefabsManager = GameManager.Instance._prefabsPoolManager;
        currentPhase = StartCoroutine(phase1());
        spawnPosSetting();
    }

    private enum MonsterSpawnPos
    {
        UP=0,DOWN,RIGHT,LEFT,RIGHTUP,RIGHTDOWN,LEFTUP,LEFTDOWN
    }

    private void spawnPosSetting()
    {
        spawnPos[(int)MonsterSpawnPos.UP] = transform.position + transform.up * spawnRange.y;
        spawnPos[(int)MonsterSpawnPos.DOWN] = transform.position - transform.up * spawnRange.y;
        spawnPos[(int)MonsterSpawnPos.RIGHT] = transform.position + transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.LEFT] = transform.position - transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.RIGHTUP] =
            transform.position
            + transform.up * spawnRange.y
            + transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.RIGHTDOWN] =
            transform.position
            - transform.up * spawnRange.y
            + transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.LEFTUP] = 
            transform.position
            + transform.up * spawnRange.y
            - transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.LEFTDOWN] = 
            transform.position 
            - transform.up * spawnRange.y
            - transform.right * spawnRange.x;
    }

    private void Interrupt()
    {
        if (_health.GetHealthRate() <= 0.1f && currentCoroutineString != "phase4")
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase4());
        }
        else if( _health.GetHealthRate() <= 0.3f && currentCoroutineString != "phase3")
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase3());
        }else if(_health.GetHealthRate() <= 0.8f && currentCoroutineString != "phase2")
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase2());
        }
        
        
    }

    private IEnumerator phase1()
    {
        currentCoroutineString = "phase1";
        Time.timeScale = 0;
        yield return CoroutineTime.GetWaitForSecondsRealtime(1);
        _dialogTyper.Enqueue("여기까지 \n잘 오셨습니다.");
        _dialogTyper.Enqueue("이..기실 수 있을까요?");
        SpawnMonster("Pumpkin",MonsterSpawnPos.LEFTDOWN);
        SpawnMonster("Pumpkin",MonsterSpawnPos.LEFTUP);
        SpawnMonster("Pumpkin",MonsterSpawnPos.RIGHTUP);
        SpawnMonster("Pumpkin",MonsterSpawnPos.RIGHTDOWN);
        yield return new WaitUntil(() => !_dialogTyper.sbWrite);
        Time.timeScale = 1;
    }

    private IEnumerator phase2()
    {
        currentCoroutineString = "phase2";
        
        _dialogTyper.WriteNow("으윽!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);

        Time.timeScale = 0;
        _dialogTyper.Enqueue("제법이시군요 \n제대로 가겠습니다");
        _dialogTyper.Enqueue("자 가자 얘들아!!");
        yield return new WaitUntil(() => !_dialogTyper.sbWrite);
        Time.timeScale = 1;
        
        SpawnMonster("SkeletonHead",MonsterSpawnPos.DOWN);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.UP);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.RIGHT);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.DOWN);
        
        
    }

    private IEnumerator phase3()
    {
        currentCoroutineString = "phase3";
        yield return null;
    }
    
    private IEnumerator phase4()
    {
        currentCoroutineString = "phase4";
        yield return null;
    }
  
    private void SpawnMonster(string name,MonsterSpawnPos positionIndex)
    {
        GameObject tmp = _prefabsManager.Get(name);
        tmp.transform.position = spawnPos[(int)positionIndex];
        tmp.SetActive(true);
        isMonsterDie[(int)positionIndex] = tmp;
    }
    
}
