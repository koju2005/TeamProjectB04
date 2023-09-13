using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class OptionBtn : MonoBehaviour
{
    PlayUIManager m;
    [SerializeField] private AudioClip buttonPressSound;
    [SerializeField] private AudioClip OpenUI;
    private void Awake()
    {
        m = PlayUIManager.instance;
    }


    public void OpenMenu()
    {
        GameManager.Instance.PlayUISound(buttonPressSound);
        if(m)
            m.OpenOptionUI();
        
    }

    public void CloseMenu()
    {
        GameManager.Instance.PlayUISound(buttonPressSound);
        if(m)   
            m.CloseOptionUI();
    }

    public void Retry()
    {
        GameManager.Instance.PlayUISound(buttonPressSound);
        if(m)
            m.RestartScene();
    }

    public void SelectScene()
    {
        GameManager.Instance.PlayUISound(buttonPressSound);
        if(m)
            m.MoveSelectScene();
    }

    public void MainScene()
    {
        GameManager.Instance.PlayUISound(buttonPressSound);
        LoadingSceneController.LoadScene(0);
    }
}
