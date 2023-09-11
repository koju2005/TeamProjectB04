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
    public float dealy;

    public ItemManager itemList;

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    // Start is called before the first frame update
    void Start()
    {
        itemList = GameManager.Instance._ItemManager;
        StartCoroutine(Drop(dealy));
    }
    // Update is called once per frame

    IEnumerator Drop(float dealy)
    {
        WaitForSeconds seconds = CoroutineTime.GetWaitForSeconds(dealy);
        int itemnum = 2;
        while (true)
        {
            System.Random randomObj = new System.Random();
            int itemType = randomObj.Next(1,itemnum);

            Vector3 spawnPos = GetRandomPosition();//랜덤위치함수
            switch (itemType) 
            {
                case 1:
                    GameObject item1 = itemList.Get("IncreaseBalls");
                    item1.SetActive(true);
                    break;
                case 2:
                    GameObject item2 = item2 = itemList.Get("HealItem");
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
