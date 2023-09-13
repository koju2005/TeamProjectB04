using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Data;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ObjectPoolManager<T> : MonoBehaviour where T : ScriptableObject
{
    private Dictionary<string, T> _objectData;
    private ObjectPool<GameObject> _pool;

    [SerializeField] private GameObject basePrefabs;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxPoolSize = 500;
    private void Awake()
    {
        _objectData = new Dictionary<string, T>();
        _pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
            OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);
    }

    public void Init(string path)
    {
        T[] resources = Resources.LoadAll<T>(path);
        foreach (var resource in resources)
        {
            _objectData.Add(resource.name,resource);
        }
    }

    public GameObject Get(string name)
    {
        GameObject newMon = _pool.Get();
        T data;
        if (_objectData.TryGetValue(name, out data))
        {
            MakeObject(newMon, data);
        }
        else
        {
            Debug.Log($"{name}은 데이터에 없는 몬스터입니다");
        }

        return newMon;
    }

    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(basePrefabs);
        poolGo.GetComponent<PooledObject>().OnRelease = OnRelease;
        return poolGo;
    }

    protected abstract void MakeObject(GameObject obj, T data);

    private void OnRelease(GameObject obj)
    {
        _pool.Release(obj);
    }

    // 사용
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public void Clear()
    {
        _pool.Clear();
    }
}
