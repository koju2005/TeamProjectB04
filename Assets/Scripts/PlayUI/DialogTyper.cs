using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DefaultNamespace;
using DefaultNamespace.Common;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogTyper : MonoBehaviour
{
    [SerializeField] public float typeSpeed=0.5f;
    [SerializeField] private float typeEndWaitTime=2;
    [SerializeField] private AudioClip[] typeSound;
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
                isSbWrite = true;
                string typeString = printQueue.Peek();
                yield return StartCoroutine(TypePrint(typeString));
                printQueue.Dequeue();
            }
            isSbWrite = false;
            yield return new WaitUntil(() => printQueue.Count > 0);
        }
    }
    
    private IEnumerator TypePrint(string typeString)
    {
        foreach (var c in typeString)
        {
            sb.Append(c);
            Dialog.text = sb.ToString();
            if (c != ' ')
            {
                int idx = Random.Range(0, typeSound.Length);
                GameManager.Instance.PlaySFX(typeSound[idx], transform.position);
            }

            yield return CoroutineTime.GetWaitForSecondsRealtime(typeSpeed);
        }
        sb.Clear();
        yield return CoroutineTime.GetWaitForSecondsRealtime(typeEndWaitTime);
        Dialog.text = "";
        
    }
    
    private IEnumerator WriteNowCoroutine(string typeString)
    {
        Dialog.text = typeString;
        yield return CoroutineTime.GetWaitForSecondsRealtime(typeEndWaitTime);
        Dialog.text = "";
    }
}
