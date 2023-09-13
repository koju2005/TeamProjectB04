using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Common;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    [SerializeField] private DialogTyper typer;

    [SerializeField] private EndingCreditStarter endingCredit;

    [SerializeField] private AudioClip endingBGM;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(scenario());
    }

    private IEnumerator scenario()
    {
        GameManager.Instance.StopBGM();
        typer.Enqueue("그렇게...");
        typer.Enqueue("저승에서 일어났었던");
        typer.Enqueue("저승인들의 도망 사건은");
        typer.Enqueue("일단락 되었다.");
        typer.Enqueue("");
        typer.Enqueue("원인 조사가 \n시작 되었지만");
        typer.Enqueue("누가 벌인 일인지\n 알 수 없었다.");
        yield return CoroutineTime.GetWaitForSecondsRealtime(1f);
        typer.Enqueue("다만 염라대왕은 엄청나게 좋아했다고 전해진다");
        typer.Enqueue("단순히 저승인들을 모두 잡아 넣어서 그랬던 것일까?");
        typer.Enqueue("흠.....");
        typer.Enqueue("");
        typer.Enqueue("과연 누가 일을 벌였던 것일까?");
        yield return new WaitUntil(() => !typer.isSbWrite);
        GameManager.Instance.PlayBGM(endingBGM);
        endingCredit.StartEndingCredit();
    }
}
