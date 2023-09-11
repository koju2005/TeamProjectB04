using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DefaultNamespace.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DongsAI : MonoBehaviour
{

    [SerializeField] private float typeSpeed=0.5f;
    [SerializeField] private float typeEndWaitTime=2;
    private TMP_Text Dialog;
    private Health health;
    private StringBuilder sb;
    
    private bool sbWrite = false;
    private bool NextOperation;
    private string currentCoroutineString;
    private Coroutine currentPhase;
    private Queue<string> printQueue;
    
    private void Awake()
    {
        health = GetComponent<Health>();
        Dialog = GetComponentInChildren<TMP_Text>();
        printQueue = new Queue<string>();
        sb = new StringBuilder(100);
        health.OnHealthChanged += Interrupt;
    }

    private void Start()
    {
        currentPhase = StartCoroutine(phase1());
        StartCoroutine(TypeQueue());
    }

    private IEnumerator TypeQueue()
    {
        while (true)
        {
            while (printQueue.Count > 0)
            {
                string typeString = printQueue.Peek();
                yield return StartCoroutine(TypePrint(typeString));
                printQueue.Dequeue();
            }
            
            yield return new WaitUntil(() => printQueue.Count > 0);
        }
    }

    private void Interrupt()
    {
        if (health.GetHealthRate() <= 0.1f && currentCoroutineString != "phase4")
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase4());
        }
        else if( health.GetHealthRate() <= 0.3f && currentCoroutineString != "phase3")
        {
            StopCoroutine(currentPhase);
            currentPhase = StartCoroutine(phase3());
        }else if(health.GetHealthRate() <= 0.8f && currentCoroutineString != "phase2")
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
        printQueue.Enqueue("안녕하세요");
        printQueue.Enqueue("이..기실 수 있을까요?");
        yield return new WaitUntil(() => printQueue.Count == 0);
        Time.timeScale = 1;
    }

    private IEnumerator phase2()
    {
        currentCoroutineString = "phase2";
        yield return StartCoroutine(WriteNow("으윽!!"));
        yield return CoroutineTime.GetWaitForSecondsRealtime(1.5f);

        Time.timeScale = 0;
        yield return StartCoroutine(TypePrint("이제부터 시작이야!"));
        yield return StartCoroutine(TypePrint("자 가자 얘들아!!"));
        yield return CoroutineTime.GetWaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        
        
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

    private IEnumerator TypePrint(string typeString)
    {
        sbWrite = true;
        foreach (var c in typeString)
        {
            sb.Append(c);
            Dialog.text = sb.ToString();
            yield return CoroutineTime.GetWaitForSecondsRealtime(typeSpeed);
        }
        sb.Clear();
        yield return CoroutineTime.GetWaitForSecondsRealtime(typeEndWaitTime);
        Dialog.text = "";
        sbWrite = false;
    }

    private IEnumerator WriteNow(string typeString)
    {
        Dialog.text = typeString;
        yield return CoroutineTime.GetWaitForSecondsRealtime(typeEndWaitTime);
    }
}
