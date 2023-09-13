using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DongsAI : MonoBehaviour
{
    [SerializeField]private Vector2 spawnRange;
    [SerializeField] private int moveSpeed=1;
    [SerializeField] private float phase4RespawnTime = 10f;
    [SerializeField] private DialogTyper _dialogTyper;
    [SerializeField] private AudioClip[] phaseMusic;
    [SerializeField] private PlayUIManager _PlayUIManager;
    //[SerializeField] private 
    private Health _health;
    private PrefabsPoolManager _prefabsManager;
    private SpriteRenderer _SpriteRenderer;
    private SoundManager _SoundManager;
    
    
    private bool NextOperation;
    private int phaseLevel = 1;
    private Coroutine currentPhase;

    private Vector3[] spawnPos = new Vector3 [8];
    private GameObject[] isMonsterDie = new GameObject[8];
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnHealthChanged += Interrupt;
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _SoundManager = GameManager.Instance._soundManager;
    }

    private void Start()
    {
        _prefabsManager = GameManager.Instance._prefabsPoolManager;
        currentPhase = StartCoroutine(phase1());
        spawnPosSetting();
    }

    private void OnDestroy()
    {
        _PlayUIManager.OptionButtonEnable(true);
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
        if (_health.GetHealth() == 1)
        {
            StopAllCoroutines();
            currentPhase = StartCoroutine(Ending());
        }else if (_health.GetHealthRate() <= 0.1f && phaseLevel <= 4)
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
    

    private IEnumerator Ending()
    {
        phaseLevel = 6;
        _PlayUIManager.OptionButtonEnable(false);
        ChangeBGM();
        Time.timeScale = 0;
        _dialogTyper.Enqueue("아..아...");
        _dialogTyper.Enqueue("아..아...");
        _dialogTyper.Enqueue(".. 나의 .. 꿈은...");
        _dialogTyper.Enqueue("나의 휴가는.....");
        _dialogTyper.Enqueue("나의 휴일은.....");
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);

        for (int i = 0; i < spawnPos.Length; ++i)
        {
            if (isMonsterDie[i])
            {
                Health monHealth = isMonsterDie[i].GetComponent<Health>();
                monHealth.AddHealth(-100);
            }
                           
        }

        int alpha = 255;
        while (alpha > 0)
        {
            alpha -= (int)(255 * Time.unscaledDeltaTime);
            _SpriteRenderer.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        yield return CoroutineTime.GetWaitForSecondsRealtime(7f);
        Time.timeScale = 1;
        _health.AddHealth(-100);
        _PlayUIManager.OptionButtonEnable(true);
    }

    private IEnumerator phase1()
    {
        phaseLevel = 1;
        ChangeBGM();
        _PlayUIManager.OptionButtonEnable(false);
        Time.timeScale = 0;
        yield return CoroutineTime.GetWaitForSecondsRealtime(1);
        _dialogTyper.Enqueue("여기까지 \n잘 오셨습니다.");
        _dialogTyper.Enqueue("저는 돌아가기 싫습니다");
        _dialogTyper.Enqueue("야근은 싫단 말입니다");
        _dialogTyper.Enqueue("");
        _dialogTyper.Enqueue("그러니");
        _dialogTyper.Enqueue("어서 나가주시죠");
        SpawnMonster("Pumpkin",MonsterSpawnPos.LEFTDOWN);
        SpawnMonster("Pumpkin",MonsterSpawnPos.LEFTUP);
        SpawnMonster("Pumpkin",MonsterSpawnPos.RIGHTUP);
        SpawnMonster("Pumpkin",MonsterSpawnPos.RIGHTDOWN);
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
        _PlayUIManager.OptionButtonEnable(true);
        Time.timeScale = 1;
    }

    private IEnumerator phase2()
    {
        phaseLevel = 2;
  
        _dialogTyper.WriteNow("으윽!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        ChangeBGM();
        _PlayUIManager.OptionButtonEnable(false);
        
        Time.timeScale = 0;
        _dialogTyper.Enqueue("약하시진 않군요");
        _dialogTyper.Enqueue("역시 사신님");
        _dialogTyper.Enqueue("이제 제대로\n가겠습니다");
        _dialogTyper.Enqueue("자 가자 얘들아!!");
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
        Time.timeScale = 1;
        
        SpawnMonster("SkeletonHead",MonsterSpawnPos.DOWN);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.UP);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.RIGHT);
        SpawnMonster("SkeletonHead",MonsterSpawnPos.LEFT);
        _PlayUIManager.OptionButtonEnable(true);
        
    }

    private IEnumerator phase3()
    {
        phaseLevel = 3;
    
        _dialogTyper.WriteNow("아아아아앜!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        
        ChangeBGM();
        _PlayUIManager.OptionButtonEnable(false);
        Time.timeScale = 0;
        _dialogTyper.Enqueue("안돼!!");
        _dialogTyper.Enqueue("야근은 싫단 말이야!!!");
        _dialogTyper.Enqueue("저리가!!! \n이 못된 자식아");
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
        Time.timeScale = 1;

        for (int i = 0; i < spawnPos.Length; ++i)
        {
            isMonsterDie[i].SetActive(true);
        }
        _PlayUIManager.OptionButtonEnable(true);
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
        ChangeBGM();
        _PlayUIManager.OptionButtonEnable(false);
        Time.timeScale = 0;
        _dialogTyper.Enqueue("안돼!!!!!!");
        _dialogTyper.Enqueue("어떻게 도망쳤는데!!!!!!");
        _dialogTyper.Enqueue("");
        _dialogTyper.Enqueue("절대 붙잡혀 줄 수 없어!!!");
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
        Time.timeScale = 1;
        _PlayUIManager.OptionButtonEnable(true);
        StartCoroutine(RespawnMonster());
        yield return null;
    }
    
    private IEnumerator phase5()
    {
        phaseLevel = 5;
        _PlayUIManager.OptionButtonEnable(false);
        _dialogTyper.WriteNow("아아아아앜!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        _dialogTyper.WriteNow("아아아아아앜!!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        _dialogTyper.WriteNow("아아아아아아앜!!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        ChangeBGM();
        
        NShooter shooter = transform.AddComponent<NShooter>();
        shooter.AttackPrefabsName = new[] { "Dongs1Weapon","Dongs2Weapon" };
        shooter.AttackDelay = new[] { 5.0f };
        shooter.RepeatCount = 10;
        shooter.RepeatTime = 0.3f;
        _PlayUIManager.OptionButtonEnable(true);
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
                if(isMonsterDie[i])
                    isMonsterDie[i].SetActive(true);            
            }
            yield return CoroutineTime.GetWaitForSeconds(phase4RespawnTime);
        }
    }

    private void ChangeBGM()
    {
        _SoundManager.PlayBGM(phaseMusic[phaseLevel]);
    }
}
