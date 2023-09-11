using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using DefaultNamespace;

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
        List<GameObject> dropItemList = new List<GameObject>();

        itemList = GameManager.Instance._ItemManager;

        item1 = itemList.Get("IncreaseBalls");
        dropItemList.Add(item1);
        item2 = itemList.Get("HealItem");
        dropItemList.Add(item2);


        //item3 = itemList.Get("name");
        //itemList2.Add(item3);
        //item4 = itemList.Get("name");
        //itemList2.Add(item4);
        StartCoroutine(Drop(dropItemList, dealy));
    }
    private void Update()
    {
       
    }

    // Update is called once per frame

    IEnumerator Drop(List<GameObject> itemList,float dealy)
    {
        while(true)
        {
            System.Random randomObj = new System.Random();
            //int randomNum = randomObj.Next(100);
            int itemType = randomObj.Next(itemList.Count);
            Vector3 spawnPos = GetRandomPosition();//랜덤위치함수
            GameObject dropItem = Instantiate(itemList[itemType], spawnPos, Quaternion.identity);
            dropItem.AddComponent<Rigidbody2D>();

            if (dropItem.transform.position.y < -5.5) //이부분이 문제인데 새로생긴 객체에 접근 하는법을 모르겠음 추후 수정예정
            {
                Destroy(dropItem);
            }

            yield return new WaitForSeconds(dealy);
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
