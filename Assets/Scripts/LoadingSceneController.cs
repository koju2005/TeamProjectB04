using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LoadingSceneController : MonoBehaviour
{

    static int nextSceneindex;
    [SerializeField]
    Image progressBar;

    public static void LoadScene(int i)
    {
        nextSceneindex = i;
        Time.timeScale = 1;
        GameManager.Instance._prefabsPoolManager.Clear();
        SceneManager.LoadScene("LoadingScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {

        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneindex);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.5f, 1f, timer);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
