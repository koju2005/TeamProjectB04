using System.Collections.Generic;
using DefaultNamespace.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace
{
    public class PrefabsPoolManager : MonoBehaviour
    {
        private Dictionary<string, GameObject> _prefabData;
        private Dictionary<string, List<GameObject>> _pool;
        
        private void Awake()
        {
            _prefabData = new Dictionary<string, GameObject>();
            _pool = new Dictionary<string, List<GameObject>>();
        }

        public void Init(string path)
        {
            GameObject[] resources = Resources.LoadAll<GameObject>(path);
            foreach (var resource in resources)
            {
                _prefabData.Add(resource.name, resource);
                //Debug.Log("resource.name");
            }
        }

        public GameObject Get(string name)
        {
            GameObject newWeapon = null;
            List<GameObject> pool;
            if (!_pool.TryGetValue(name, out pool))
            {
                pool = new List<GameObject>();
                _pool.Add(name,pool);
            }
            
            newWeapon = FindObject(pool);
            if (newWeapon == null)
            {
                newWeapon = CreateGameObject(name);
                pool.Add(newWeapon);
            }

            newWeapon.GetComponent<PooledObject>().OnRelease += OnRelease;
            
            return newWeapon;
        }
        private void OnRelease(GameObject poolGo)
        {
            poolGo.SetActive(false);
        }

        // 사용
        private void OnTakeFromPool(GameObject poolGo)
        {
            poolGo.SetActive(true);
        }
        
        private GameObject FindObject(List<GameObject> pool)
        {
            GameObject find = null;

            foreach (var obj in pool)
            {
                if (!obj.activeSelf)
                {
                    find = obj;
                    break;
                }
            }
            return find;
        }

        private GameObject CreateGameObject(string name)
        {
            GameObject newWeapon = null;
            GameObject prefab;
            if (_prefabData.TryGetValue(name, out prefab))
            {
                newWeapon = Instantiate(prefab);
                newWeapon.SetActive(false);
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"{name}은 데이터에 없는 프리팹입니다.");
#endif
            }

            return newWeapon;
        }
    }
        
}