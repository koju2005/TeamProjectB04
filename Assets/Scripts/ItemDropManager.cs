using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using DefaultNamespace;
using DefaultNamespace.Common;

public class ItemDropManager : MonoBehaviour
{
    public GameObject _dropPosition;
    public float dealy=20;

    public ItemManager itemList;

    // Start is called before the first frame update
    void Start()
    {
        itemList = GameManager.Instance._ItemManager;
        Invoke("Wait",3f);
        //StartCoroutine(Drop(dealy));

    }
    // Update is called once per frame
    public void Wait()
    {
        Debug.Log("시작체크");
        StartCoroutine(Drop(dealy));
    }
    IEnumerator Drop(float dealy)
    {
        WaitForSeconds seconds = CoroutineTime.GetWaitForSeconds(dealy);
        int itemCount = 2;
        while (true)
        {
            System.Random randomObj = new System.Random();
            int itemType = randomObj.Next(0,itemCount);
            Debug.Log(itemType);
            Vector3 spawnPos = GetRandomPosition();//랜덤위치함수
            switch (itemType) 
            {
                case 0:
                    GameObject item1 = itemList.Get("IncreaseBalls");
                    item1.transform.position = spawnPos;
                    item1.SetActive(true);
                    break;
                case 1:
                    GameObject item2 = item2 = itemList.Get("HealItem");
                    item2.transform.position = spawnPos;
                    item2.SetActive(true);
                    break;
            }
            //yield return new WaitForSeconds(dealy);

                yield return seconds;
        }

    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition;
        basePosition.x = 0f;
        basePosition.y = 4f;
        basePosition.z = 0f;

        float posX = basePosition.x + Random.Range(-2.5f, 2.5f);

        Vector3 spawnPos = new Vector3(posX, basePosition.y, basePosition.z);

        return spawnPos;
    }
}
