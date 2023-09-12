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
    [SerializeField] private int moveSpeed=1;
    [SerializeField] private float phase4RespawnTime = 10f;
    [SerializeField]private DialogTyper _dialogTyper;
    private Health _health;
    private PrefabsPoolManager _prefabsManager;
    
    
    private bool NextOperation;
    private int phaseLevel = 1;
    private Coroutine currentPhase;

    private Vector3[] spawnPos = new Vector3 [8];
    private GameObject[] isMonsterDie = new GameObject[8];
    
    private void Awake()
    {
        _health = GetComponent<Health>();
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
        if (_health.GetHealthRate() <= 0.1f && phaseLevel <= 4)
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase5());
        }else if (_health.GetHealthRate() <= 0.3f && phaseLevel <= 3 )
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase4());
        }
        else if( _health.GetHealthRate() <= 0.5f && phaseLevel <= 2)
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase3());
        }else if(_health.GetHealthRate() <= 0.8f && phaseLevel <= 1)
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase2());
        }
    }

    

    private IEnumerator phase1()
    {
        phaseLevel = 1;
        Time.timeScale = 0;
        yield return CoroutineTime.GetWaitForSecondsRealtime(1);
        _dialogTyper.Enqueue("여기까지 \n잘 오셨습니다.");
        _dialogTyper.Enqueue("저는 돌아가기 싫어요");
        _dialogTyper.Enqueue("야근은 싫단 말이예요");
        _dialogTyper.Enqueue("");
        _dialogTyper.Enqueue("그러니");
        _dialogTyper.Enqueue("어서 나가주시죠");
        SpawnMonster("Pumpkin",MonsterSpawnPos.LEFTDOWN);
        SpawnMonster("Pumpkin",MonsterSpawnPos.LEFTUP);
        SpawnMonster("Pumpkin",MonsterSpawnPos.RIGHTUP);
        SpawnMonster("Pumpkin",MonsterSpawnPos.RIGHTDOWN);
        yield return new WaitUntil(() => !_dialogTyper.sbWrite);
        Time.timeScale = 1;
    }

    private IEnumerator phase2()
    {
        phaseLevel = 2;
        
        _dialogTyper.WriteNow("으윽!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);

        Time.timeScale = 0;
        _dialogTyper.Enqueue("약하시진 않군요");
        _dialogTyper.Enqueue("역시 사신님");
        _dialogTyper.Enqueue("이제 제대로\n가겠습니다");
        _dialogTyper.Enqueue("자 가자 얘들아!!");
        yield return new WaitUntil(() => !_dialogTyper.sbWrite);
        Time.timeScale = 1;
        
        SpawnMonster("SkeletonHead",MonsterSpawnPos.DOWN);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.UP);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.RIGHT);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.LEFT);
        
        
    }

    private IEnumerator phase3()
    {
        phaseLevel = 3;
        
        _dialogTyper.WriteNow("아아아아앜!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        Time.timeScale = 0;
        _dialogTyper.Enqueue("안돼!!");
        _dialogTyper.Enqueue("야근은 싫단 말이야!!!");
        yield return new WaitUntil(() => !_dialogTyper.sbWrite);
        Time.timeScale = 1;

        for (int i = 0; i < spawnPos.Length; ++i)
        {
            isMonsterDie[i].SetActive(true);
        }

        StartCoroutine(MoveSideStep());
        yield return null;
    }
    
    private IEnumerator phase4()
    {
        phaseLevel = 4;
        
        _dialogTyper.WriteNow("아아아아앜!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        _dialogTyper.WriteNow("아아아아아앜!!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        Time.timeScale = 0;
        _dialogTyper.Enqueue("이럴 수는 없다구!!!!!!!");
        _dialogTyper.Enqueue("어떻게 도망쳤는데!!!!!!");
        _dialogTyper.Enqueue("");
        yield return new WaitUntil(() => !_dialogTyper.sbWrite);
        Time.timeScale = 1;
        StartCoroutine(RespawnMonster());
        yield return null;
    }
    
    private IEnumerator phase5()
    {
        phaseLevel = 5;
        
        _dialogTyper.WriteNow("아아아아앜!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        _dialogTyper.WriteNow("아아아아아앜!!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        _dialogTyper.WriteNow("아아아아아아앜!!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        yield return null;
    }
  
    private void SpawnMonster(string name,MonsterSpawnPos positionIndex)
    {
        GameObject tmp = _prefabsManager.Get(name);
        tmp.transform.position = spawnPos[(int)positionIndex];
        tmp.SetActive(true);
        isMonsterDie[(int)positionIndex] = tmp;
    }

    private IEnumerator MoveSideStep()
    {
        Vector3 RightPos = spawnPos[(int)MonsterSpawnPos.RIGHT];
        Vector3 LeftPos = spawnPos[(int)MonsterSpawnPos.LEFT];
        Vector3 targetPos = RightPos;
        while (true)
        {
            transform.position += (targetPos - transform.position).normalized * moveSpeed * Time.deltaTime; 
            if (targetPos.sqrMagnitude < transform.position.sqrMagnitude)
            {
                if (RightPos == targetPos)
                    targetPos = LeftPos;
                else
                    targetPos = RightPos;
            }
            yield return null;
        }
    }

    private IEnumerator RespawnMonster()
    {
        while (true)
        {
            for (int i = 0; i < spawnPos.Length; ++i)
            {
                isMonsterDie[i].SetActive(true);            
            }
            yield return CoroutineTime.GetWaitForSeconds(phase4RespawnTime);
        }
        
    }

}
