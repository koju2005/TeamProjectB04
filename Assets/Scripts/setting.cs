using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setting : MonoBehaviour
{
    public GameObject MonsterList;
    public GameObject monster;

    // Start is called before the first frame update
    //
    //
    void Start()
    {
        int maxSize = 5;// ��ġ�� ������ 
        int currentStage = 3;//���������� ���� ����Ƚ���� �ٸ�

        //if (currentStage >= 2) { currentStage = 2;} 

        int[] loop = { 2, 3, 4, 5, 6, 7 };// 4 x 4 �� �������� ������? �÷����غ��� �ҵ��� 
        float between = 0.05f;
        //                  �ִ�Ÿ� - (������ �Ÿ� * ���ٴ� ��������(�����̶� -1������)) /��������
        float monsterScale = (maxSize - (between * (loop[currentStage] - 1))) / loop[currentStage];

        for (int i = 0; i < loop[currentStage]; ++i)
        {
            for (int j = 0; j < loop[currentStage]; ++j)
            {
                GameObject newMonster = Instantiate(monster);

                newMonster.transform.parent = GameObject.Find("MonsterList").transform;
                newMonster.transform.position = new Vector3((i * (monsterScale + between)), (j * (monsterScale + between)), 1);
            }
        }
        Vector3 leftDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));// ī�޶� ����  0 0 0 ��ǥ ��� ������ ��� �Ѹ���
                                         //�̺κ� ���� ���� ��û�ҵ�
        MonsterList.transform.position = leftDown + new Vector3(0.5f + monsterScale / 2, 3f, 0);
                                        //�����غ��ϱ� y�� ���ͽ����� �ʿ� �����ʳ� �ͱ⵵��?
                                        //x�� �ʿ����?
                                        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
