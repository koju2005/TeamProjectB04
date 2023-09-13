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


    private SoundManager _SoundManager;
    [SerializeField] private AudioClip[] phaseMusic;


    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnHealthChanged += Interrupt;

        _health.OnHealthChanged += Hit_anim;
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _anim = _SpriteRenderer.GetComponent<Animator>();

        _SoundManager = GameManager.Instance._soundManager;
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
            currentPhase = StartCoroutine(phase3());
        }
        else if (_health.GetHealthRate() <= 0.8f && phaseLevel <= 1)
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase2());
        }
    }


    private IEnumerator Ending()
    {
        phaseLevel = 4;
        ChangeBGM();
        Time.timeScale = 0;
        _dialogTyper.Enqueue("°ÅÀÇ ´Ù µµ¸ÁÃÆ´Âµ¥...");
        _dialogTyper.Enqueue("ÀÌ ¸ÁÇÒÀÚ½Ä..!!");
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);

        for (int i = 0; i < spawnPos.Length; ++i)
        {
            if (isMonsterDie[i])
            {
                Health monHealth = isMonsterDie[i].GetComponent<Health>();
                monHealth.AddHealth(-100);
            }

        }

        _anim.SetBool("isDead", true);
        _dialogTyper.Enqueue("²ô¾Æ¾Æ¾Æ¾Æ¾Æ¾Æ¾Ç!!!!");

        yield return CoroutineTime.GetWaitForSecondsRealtime(6f);
        Time.timeScale = 1;
        _health.AddHealth(-100);
    }

    private IEnumerator phase1()
    {
        phaseLevel = 1;
        ChangeBGM();
        Time.timeScale = 0;
        yield return CoroutineTime.GetWaitForSecondsRealtime(1);
        _dialogTyper.Enqueue("¹ú½á ¿©±â±îÁö ¦i¾Æ¿Ô³ª..");
        _dialogTyper.Enqueue("³×³ðÀ» ¹Ú»ì³»ÁÖ¸¶!");
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
        ChangeBGM();
        _dialogTyper.WriteNow("À¸¾Ç!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);

        Time.timeScale = 0;
        _dialogTyper.Enqueue("°ö°Ô ÀâÈ÷Áø ¾Ê°Ú´Ù!");
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
        Time.timeScale = 1;
        SpawnMonster("FireGuy", MonsterSpawnPos.RIGHT);
        SpawnMonster("FireGuy", MonsterSpawnPos.lEFT);


        for (int i = 0; i < spawnPos.Length; ++i)
        {
            isMonsterDie[i].SetActive(true);
        }


    }


    private IEnumerator phase3()
    {
        phaseLevel = 3;

        _dialogTyper.WriteNow("Á×¾î¶ó!!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);

        _anim.SetTrigger("isAttack");
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

    private void ChangeBGM()
    {
        _SoundManager.PlayBGM(phaseMusic[phaseLevel]);
    }

}
