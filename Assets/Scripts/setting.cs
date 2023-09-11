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
        int maxSize = 5;// 배치될 사이즈 
        int currentStage = 3;//스테이지에 따라 도는횟수가 다름

        //if (currentStage >= 2) { currentStage = 2;} 

        int[] loop = { 2, 3, 4, 5, 6, 7 };// 4 x 4 도 좀많을수 있을듯? 플레이해봐야 할듯함 
        float between = 0.05f;
        //                  최대거리 - (몹사이 거리 * 한줄당 몹마리수(간격이라 -1마리임)) /몹마리수
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
        Vector3 leftDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));// 카메라 기준  0 0 0 좌표 잡고 스케일 비례 뿌리기
                                         //이부분 시행 착오 엄청할듯
        MonsterList.transform.position = leftDown + new Vector3(0.5f + monsterScale / 2, 3f, 0);
                                        //생각해보니까 y는 몬스터스케일 필요 없지않나 싶기도함?
                                        //x도 필요없나?
                                        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
