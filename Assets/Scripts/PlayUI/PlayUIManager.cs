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

    GameObject Player;

    [SerializeField] private GameObject GamePlayUI;
    [SerializeField] private GameObject LoseUI;
    [SerializeField] private GameObject WinUI;
    [SerializeField] private GameObject OptionUI;

    private GameObject gamePlayUI;
    private GameObject loseUI;
    private GameObject winUI;
    private GameObject optionUI;


    private void Awake()
    {
        LoseUI.SetActive(false);
        WinUI.SetActive(false);
        OptionUI.SetActive(false);

        gamePlayUI = GameObject.Instantiate(GamePlayUI);
        loseUI = GameObject.Instantiate(LoseUI);
        winUI = GameObject.Instantiate(WinUI);
        optionUI = GameObject.Instantiate(OptionUI);

        Player = GameManager.Instance.GetPlayer();
        Player.GetComponent<Health>().OnDeath += Lose;
    }

    private void Lose(string tag)
    {
        loseUI.SetActive(true);
    }

    private void Win(string tag)
    {
        if(tag == "boss")
        {
            winUI.SetActive(true);
        }
    }

    public void OpenOptionUI()
    {
        optionUI.SetActive(true);
    }

    public void CloseOptionUI()
    {
        optionUI.SetActive(false);
    }

    public void MoveSelectScene()
    {
        SceneManager.LoadScene("SelcectScene");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
