using DefaultNamespace;
using DefaultNamespace.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LKHAI : MonoBehaviour
{

    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private float phase4RespawnTime = 10f;
    [SerializeField] private DialogTyper _dialogTyper;
    [SerializeField] private PlayUIManager _PlayUIManager;
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



    private IEnumerator phase1()
    {
        phaseLevel = 1;
        ChangeBGM();
        _PlayUIManager.OptionButtonEnable(false);
        Time.timeScale = 0;
        yield return CoroutineTime.GetWaitForSecondsRealtime(1);
        _dialogTyper.Enqueue("벌써 여기까지\n쫒아왔나..");
        _dialogTyper.Enqueue("날 잡으려는걸\n후회하게 해주마!");
        SpawnMonster("Bat", MonsterSpawnPos.B1);
        SpawnMonster("Bat", MonsterSpawnPos.B2);
        SpawnMonster("Bat", MonsterSpawnPos.B3);
        SpawnMonster("Bat", MonsterSpawnPos.B4);
        SpawnMonster("Bat", MonsterSpawnPos.B5);
        SpawnMonster("Bat", MonsterSpawnPos.B6);
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
        _PlayUIManager.OptionButtonEnable(true);
        Time.timeScale = 1;
    }

    private IEnumerator phase2()
    {
        phaseLevel = 2;
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);
        ChangeBGM();
        _PlayUIManager.OptionButtonEnable(false);
        Time.timeScale = 0;
        _dialogTyper.Enqueue("으악!");
        _dialogTyper.Enqueue("곱게 잡히진 않겠다!");
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);
        Time.timeScale = 1;
        _PlayUIManager.OptionButtonEnable(true);
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

        _dialogTyper.WriteNow("죽어라!!");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);

        NShooter shooter = transform.AddComponent<NShooter>();
        shooter.AttackPrefabsName = new[] { "Dongs1Weapon", "Dongs2Weapon" };
        shooter.AttackDelay = new[] { 5.0f };
        shooter.RepeatCount = 10;
        shooter.RepeatTime = 0.3f;

        yield return null;
    }


    private IEnumerator Ending()
    {
        phaseLevel = 4;
        _PlayUIManager.OptionButtonEnable(false);
        Time.timeScale = 0;
        _dialogTyper.Enqueue("거의 다 도망쳤는데...");
        _dialogTyper.Enqueue("이 망할자식..!!");
        yield return new WaitUntil(() => !_dialogTyper.isSbWrite);

        for (int i = 0; i < spawnPos.Length; ++i)
        {
            if (isMonsterDie[i])
            {
                Health monHealth = isMonsterDie[i].GetComponent<Health>();
                monHealth.AddHealth(-100);
            }

        }

        ChangeBGM();
        _anim.SetBool("isDead", true);
        _dialogTyper.Enqueue("끄아아아아아아악!!!!");

        yield return CoroutineTime.GetWaitForSecondsRealtime(2f);
        _SoundManager.StopBGM();
        Time.timeScale = 1;
        _PlayUIManager.OptionButtonEnable(true);
        _health.AddHealth(-100);
    }

    private void SpawnMonster(string name, MonsterSpawnPos positionIndex)
    {
        GameObject tmp = _prefabsManager.Get(name);
        tmp.transform.position = spawnPos[(int)positionIndex];
        tmp.SetActive(true);
        isMonsterDie[(int)positionIndex] = tmp;
    }



    private void ChangeBGM()
    {
        _SoundManager.PlayBGM(phaseMusic[phaseLevel]);
    }

    private void OnDisable()
    {
        _SoundManager.StopBGM();
    }

}
