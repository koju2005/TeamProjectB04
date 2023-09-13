using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Common;
using UnityEngine;

public class EndingCreditStarter : MonoBehaviour
{
    [SerializeField] private float rollSpeed;
    [SerializeField] private DialogTyper dialogTyper;

    private RectTransform rollPage;

    private void Awake()
    {
        rollPage = GetComponent<RectTransform>();
    }

    public void StartEndingCredit()
    {
        StartCoroutine(EndingCredit());
    }
    
    public IEnumerator EndingCredit()
    {
        while (rollPage.anchoredPosition.y <= rollPage.rect.height)
        {
            rollPage.position += rollPage.up * rollSpeed * Time.unscaledDeltaTime;
            yield return null;
        }

        dialogTyper.typeSpeed = 0.5f;
        dialogTyper.Enqueue("END");

        yield return new WaitUntil(() => !dialogTyper.isSbWrite);
        yield return CoroutineTime.GetWaitForSecondsRealtime(3);
        dialogTyper.typeSpeed = 0.2f;

        GameManager.Instance.Reset();
        LoadingSceneController.LoadScene(0);
    }
}
