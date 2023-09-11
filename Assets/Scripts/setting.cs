using DefaultNamespace;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setting : MonoBehaviour
{
    public GameObject MonsterList;
    public GameObject monster;
    public int count;


    // Start is called before the first frame update
    //
    //
    void Start()
    {
        //monster = GameManager.Instance. �⺻ ���Ͱ� �������� �����ؾ���
        MonsterList = new GameObject();
        MonsterList.name = "MonsterList";
        int maxSize = 5;// ��ġ�� ������ 
        int currentStage = count;//���������� ���� ����Ƚ���� �ٸ�

        if (currentStage >= 3) { currentStage = 3;} 

        int[] loop = { 2, 3, 4, 5};
        float between = 0.05f;
        //                  
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
        Vector3 leftDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        MonsterList.transform.position = leftDown + new Vector3(0.5f + monsterScale / 2, 3f, 0);

                                        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
