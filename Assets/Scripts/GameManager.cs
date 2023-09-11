using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public ItemManager _ItemManager { get; private set; }
        public PrefabsPoolManager _prefabsPoolManager { get; private set; }
        
        public HashSet<GameObject> _currentWeapons { get; private set; }
        
        private static GameManager _instance;
        private GameObject _player;
        public static bool isApplicationExit = false;
        public int _stageIndex;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("GameManager");
                        _instance = obj.AddComponent<GameManager>();
                    }                        
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        private void Awake()
        {
            _ItemManager = GetComponent<ItemManager>();
            _prefabsPoolManager = GetComponent<PrefabsPoolManager>();
            _currentWeapons = new HashSet<GameObject>();
        }

        private void Start()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            _instance = this;
            _prefabsPoolManager.Init(@"Prefabs");
            _ItemManager.Init(@"Scriptable\Items");
            DontDestroyOnLoad(gameObject);
        }
        
        public GameObject GetPlayer()
        {
            if (!_player)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
            }
            return _player;
        }

        public void OnDisable()
        {
            isApplicationExit = true;
        }


        public void CheckDeathCount(string whoisDeadTag)
        {
            
        }

        public void AddWeapon(GameObject weapon)
        {
            _currentWeapons.Add(weapon);
        }

        public void RemoveWeapon(GameObject weapon)
        {
            _currentWeapons.Remove(weapon);
        }

        public HashSet<GameObject>.Enumerator GetWeapons()
        {
            return _currentWeapons.GetEnumerator();
        }

        public void SetSelectedStageIndex(int stageindex)
        {
            _stageIndex = stageindex;
        }

       public void LoadScene()
        {
            LoadingSceneController.LoadScene(_stageIndex);
        }
    }
}