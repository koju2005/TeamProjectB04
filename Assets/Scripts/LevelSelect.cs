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
                    //���⼭ ���� ������ Ŭ�����ϸ� �Ʒ� ���������
                    Transform stoneTransform = hit.collider.transform.Find("Stone");
                    Transform flameTransform = hit.collider.transform.Find("Flame");
                    flameTransform.gameObject.SetActive(false);
                    Renderer stoneRenderer = stoneTransform.GetComponent<Renderer>();
                    stoneRenderer.material.color = new Color(0,0,0,0.7f);
                   //�������
                   //���ӸŴ������� IsWin �޼��� �Ἥ ���λ����ɵ�
                
                    //GameManager.SetSelectedStageIndex(_stageindex);
                }
            }
            else
            {
                Debug.Log("����");
            }
        }
    }
}
