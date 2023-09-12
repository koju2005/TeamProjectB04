using DefaultNamespace;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayUIManager : MonoBehaviour
{
    private static PlayUIManager m_instance;

    public static PlayUIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PlayUIManager>();
            }

            return m_instance;
        }
    }

    GameManager gameManager;
    GameObject Player;

    [SerializeField] private GameObject GamePlayUI;
    [SerializeField] private GameObject LoseUI;
    [SerializeField] private GameObject WinUI;
    [SerializeField] private GameObject OptionUI;

    private GameObject gamePlayUI;
    private GameObject loseUI;
    private GameObject winUI;
    private GameObject optionUI;

    int _stageIndex;



    private void Awake()
    {
        _stageIndex = GameManager.Instance._stageIndex;
        Debug.Log("�������� : " + _stageIndex.ToString());
        LoseUI.SetActive(false);
        WinUI.SetActive(false);
        OptionUI.SetActive(false);

        gamePlayUI = GameObject.Instantiate(GamePlayUI);
        loseUI = GameObject.Instantiate(LoseUI);
        winUI = GameObject.Instantiate(WinUI);
        optionUI = GameObject.Instantiate(OptionUI);

        gameManager = GameManager.Instance;
        Player = gameManager.GetPlayer();
        
        Player.GetComponent<Health>().OnDeath += Lose;
        gameManager.win += Win;
        gameManager.lose += Lose;
    }

    public void Lose(string tag)
    {
        Time.timeScale = 0;
        if(loseUI !=null)
        loseUI.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0;
        winUI.SetActive(true);

    }

    public void OpenOptionUI()
    {
        Time.timeScale = 0;
        optionUI.SetActive(true);
    }

    public void CloseOptionUI()
    {
        optionUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void MoveSelectScene()
    {
        optionUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
        Time.timeScale = 1;
        gameManager.LoadSelecteScene();
    }

    public void RestartScene()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
        Time.timeScale = 1;
        LoadingSceneController.LoadScene(_stageIndex);
    }

}
