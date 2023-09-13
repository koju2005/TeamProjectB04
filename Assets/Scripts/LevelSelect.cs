using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{

    public AudioClip Stage1;
    public AudioClip Stage2;
    public AudioClip Stage3;
    public AudioClip Stage4;
    public AudioClip Stage5;
    AudioSource audioSource;
    GameObject[] StageObject;
    private static LevelSelect instance = null;
    bool[] stageclear = new bool[5];
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        StageClearCheck();
        AllClearCheck();
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
         
                if(hit.collider.tag == "Stage" && hit.collider.GetComponent<StageIndex>().StageClear == false)
                {
                    
                    int _stageindex = hit.collider.GetComponent<StageIndex>().stageindex;
                    GameManager.Instance.SetSelectedStageIndex(_stageindex);
                    SoundSelect();
                    audioSource.Play();
                    GameManager.Instance.LoadScene();
                   
                }
            }
        }
    }

    void SoundSelect()
    {
        int index = GameManager.Instance._stageIndex;
        switch (index) 
        {   
            case 2:
              audioSource.clip = Stage1;
                break;
            case 3:
                audioSource.clip = Stage2;
                break;
            case 4:
                audioSource.clip = Stage3;
                break;
            case 5:
                audioSource.clip = Stage4;
                break;
            case 6:
                audioSource.clip = Stage5;
                break;
        }
    }

    void StageClearCheck()
    {
        StageObject = GameObject.FindGameObjectsWithTag("Stage");
        for(int i = 0; i < 5; i++)
        {
            stageclear[i] = GameManager.Instance.stageClear[i];
        }

        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5;j++)
            {
                if (stageclear[i] == true && StageObject[j].gameObject.GetComponent<StageIndex>().stageindex == i+2)
                {
                    Transform stoneTransform = StageObject[j].gameObject.transform.Find("Stone");
                    Transform flameTransform = StageObject[j].gameObject.transform.Find("Flame");
                    StageObject[j].gameObject.GetComponent<StageIndex>().StageClear = true;
                    flameTransform.gameObject.SetActive(false);
                    Renderer stoneRenderer = stoneTransform.GetComponent<Renderer>();
                    stoneRenderer.material.color = new Color(0, 0, 0, 0.7f);
                }
            }

        }
    }
     void AllClearCheck()
    {
        bool allClear = true;
        foreach (bool ck in GameManager.Instance.stageClear)
        {
            if (!ck)
            {
                allClear = false;
                break; 
            }
        }
        if (allClear)
            {
                LoadingSceneController.LoadScene(0); // 나중에 추가되는 엔딩씬 인덱스번호 0지우고 넣어주시면 됩니당. -강현
            }
    }
}
