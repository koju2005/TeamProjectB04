using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{

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
                    //여기서 만약 스테이 클리어하면 아래 구문쓰면됨
                    Transform stoneTransform = hit.collider.transform.Find("Stone");
                    Transform flameTransform = hit.collider.transform.Find("Flame");
                    flameTransform.gameObject.SetActive(false);
                    Renderer stoneRenderer = stoneTransform.GetComponent<Renderer>();
                    stoneRenderer.material.color = new Color(0,0,0,0.7f);
                   //여기까지
                   //게임매니저에서 IsWin 메서드 써서 따로빼도될듯
                
                    //GameManager.SetSelectedStageIndex(_stageindex);
                }
            }
            else
            {
                Debug.Log("없음");
            }
        }
    }
}
