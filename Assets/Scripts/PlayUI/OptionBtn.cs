using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBtn : MonoBehaviour
{
    PlayUIManager m;

    private void Awake()
    {
        m = PlayUIManager.instance;
    }


    public void OpenMenu()
    {
        m.OpenOptionUI();
    }

    public void CloseMenu()
    {
        m.CloseOptionUI();
    }

    public void Retry()
    {
        m.RestartScene();
    }

    public void SelectScene()
    {
        m.MoveSelectScene();
    }
}
