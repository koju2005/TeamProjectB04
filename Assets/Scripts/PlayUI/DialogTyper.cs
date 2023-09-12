using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DefaultNamespace.Common;
using TMPro;
using UnityEngine;

public class DialogTyper : MonoBehaviour
{
    [SerializeField] private float typeSpeed=0.5f;
    [SerializeField] private float typeEndWaitTime=2;
    
    private Queue<string> printQueue;
    private StringBuilder sb;
    private TMP_Text Dialog;

    public bool isSbWrite { get; private set; } = false;

    private void Awake()
    {
        Dialog = GetComponentInChildren<TMP_Text>();
        printQueue = new Queue<string>();
        sb = new StringBuilder(100);
    }

    private void Start()
    {
        StartCoroutine(TypeQueue());
    }

    public void Enqueue(string typeString)
    {
        printQueue.Enqueue(typeString);
        isSbWrite = true;
    }

    public void WriteNow(string nowString)
    {
        StartCoroutine(WriteNowCoroutine(nowString));
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
    
    private IEnumerator TypePrint(string typeString)
    {
        isSbWrite = true;
        foreach (var c in typeString)
        {
            sb.Append(c);
            Dialog.text = sb.ToString();
            yield return CoroutineTime.GetWaitForSecondsRealtime(typeSpeed);
        }
        sb.Clear();
        yield return CoroutineTime.GetWaitForSecondsRealtime(typeEndWaitTime);
        Dialog.text = "";
        isSbWrite = false;
    }
    
    private IEnumerator WriteNowCoroutine(string typeString)
    {
        Dialog.text = typeString;
        yield return CoroutineTime.GetWaitForSecondsRealtime(typeEndWaitTime);
        Dialog.text = "";
    }
}
