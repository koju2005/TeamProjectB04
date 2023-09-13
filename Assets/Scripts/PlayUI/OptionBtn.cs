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
        GameManager.Instance.PlayUI(buttonPressSound);
        m.OpenOptionUI();
    }

    public void CloseMenu()
    {
        GameManager.Instance.PlayUI(buttonPressSound);
        m.CloseOptionUI();
    }

    public void Retry()
    {
        GameManager.Instance.PlayUI(buttonPressSound);
        m.RestartScene();
    }

    public void SelectScene()
    {
        GameManager.Instance.PlayUI(buttonPressSound);
        m.MoveSelectScene();
    }
}
