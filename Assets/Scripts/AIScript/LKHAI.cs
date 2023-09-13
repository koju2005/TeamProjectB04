using DefaultNamespace;
using DefaultNamespace.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LKHAI : MonoBehaviour
{

    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private int moveSpeed = 1;
    [SerializeField] private float phase4RespawnTime = 10f;
    [SerializeField] private DialogTyper _dialogTyper;
    private Health _health;
    private PrefabsPoolManager _prefabsManager;
    private SpriteRenderer _SpriteRenderer;
    private Animator _anim;


    private bool NextOperation;
    private int phaseLevel = 1;
    private Coroutine currentPhase;

    private Vector3[] spawnPos = new Vector3[8];
    private GameObject[] isMonsterDie = new GameObject[8];

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnHealthChanged += Interrupt;
        _health.OnHealthChanged += Hit_anim;
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _anim = _SpriteRenderer.GetComponent<Animator>();
    }

    private void Start()
    {
        _prefabsManager = GameManager.Instance._prefabsPoolManager;
        currentPhase = StartCoroutine(phase1());
        spawnPosSetting();
    }

    private enum MonsterSpawnPos
    {
        B1 = 0, B2, B3, B4, B5, B6, lEFT, RIGHT
    }

    private void spawnPosSetting()
    {

        spawnPos[(int)MonsterSpawnPos.B1] = 
            transform.position 
            - transform.up * spawnRange.y
            - transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.B2] = 
            transform.position 
            - transform.up * spawnRange.y;
        spawnPos[(int)MonsterSpawnPos.B3] = 
            transform.position
            - transform.up * spawnRange.y
            + transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.B4] =
            transform.position
            - transform.up * spawnRange.y
            - transform.up * spawnRange.y
            - transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.B5] =
            transform.position
            - transform.up * spawnRange.y
            - transform.up * spawnRange.y;
        spawnPos[(int)MonsterSpawnPos.B6] =
            transform.position
            - transform.up * spawnRange.y
            - transform.up * spawnRange.y
            + transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.lEFT] =
            transform.position
            - transform.right * spawnRange.x;
        spawnPos[(int)MonsterSpawnPos.RIGHT] =
            transform.position
            + transform.right * spawnRange.x;
    }

    private void Hit_anim()
    {
        _anim.SetTrigger("isHit");
    }

    private void Interrupt()
    {
        if (_health.GetHealth() == 1)
        {
            StopAllCoroutines();
            currentPhase = StartCoroutine(Ending());
        }
        else if (_health.GetHealthRate() <= 0.5f && phaseLevel <= 2)
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase5());
        }
        else if (_health.GetHealthRate() <= 0.8f && phaseLevel <= 1)
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase2());
        }
    }


    private IEnumerator Ending()
    {
        phaseLevel = 6;
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
    }

    private IEnumerator phase1()
    {
        phaseLevel = 1;
        Time.timeScale = 0;
        yield return CoroutineTime.GetWaitForSecondsRealtime(1);
        _dialogTyper.Enqueue("여기까지 \n잘 오셨습니다.");
        SpawnMonster("Bat", MonsterSpawnPos.B1);
        SpawnMonster("Bat", MonsterSpawnPos.B2);
        SpawnMonster("Bat", MonsterSpawnPos.B3);
        SpawnMonster("Bat", MonsterSpawnPos.B4);
        SpawnMonster("Bat", MonsterSpawnPos.B5);
        SpawnMonster("Bat", MonsterSpawnPos.B6);
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
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
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
        Time.timeScale = 1;
        SpawnMonster("FireGuy", MonsterSpawnPos.RIGHT);
        SpawnMonster("FireGuy", MonsterSpawnPos.lEFT);


        for (int i = 0; i < spawnPos.Length; ++i)
        {
            isMonsterDie[i].SetActive(true);
        }


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

        NShooter shooter = transform.AddComponent<NShooter>();
        shooter.AttackPrefabsName = new[] { "Dongs1Weapon", "Dongs2Weapon" };
        shooter.AttackDelay = new[] { 5.0f };
        shooter.RepeatCount = 10;
        shooter.RepeatTime = 0.3f;

        yield return null;
    }

    private void SpawnMonster(string name, MonsterSpawnPos positionIndex)
    {
        GameObject tmp = _prefabsManager.Get(name);
        tmp.transform.position = spawnPos[(int)positionIndex];
        tmp.SetActive(true);
        isMonsterDie[(int)positionIndex] = tmp;
    }

    private IEnumerator MoveSideStep()
    {
        Vector3 RightPos = spawnPos[(int)MonsterSpawnPos.B3];
        Vector3 LeftPos = spawnPos[(int)MonsterSpawnPos.B4];
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
                if (isMonsterDie[i])
                    isMonsterDie[i].SetActive(true);
            }
            yield return CoroutineTime.GetWaitForSeconds(phase4RespawnTime);
        }
    }




}
