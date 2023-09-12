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
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
         
                if(hit.collider.tag == "Stage")
                {
                    int _stageindex = hit.collider.GetComponent<StageIndex>().stageindex;
                    GameManager.Instance.endKey = false;
                    //���⼭ ���� ������ Ŭ�����ϸ� �Ʒ� ���������
                    Transform stoneTransform = hit.collider.transform.Find("Stone");
                    Transform flameTransform = hit.collider.transform.Find("Flame");
                    flameTransform.gameObject.SetActive(false);
                    Renderer stoneRenderer = stoneTransform.GetComponent<Renderer>();
                    stoneRenderer.material.color = new Color(0,0,0,0.7f);
                    //�������
                    //���ӸŴ������� IsWin �޼��� �Ἥ ���λ����ɵ�
                    
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
            case 4:
              audioSource.clip = Stage1;
                break;
            case 5:
                audioSource.clip = Stage2;
                break;
            case 6:
                audioSource.clip = Stage3;
                break;
            case 7:
                audioSource.clip = Stage4;
                break;
            case 8:
                audioSource.clip = Stage5;
                break;
        }
    }
}
