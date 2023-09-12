using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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

        public int monsterCount = 0;
        private bool stageClear1;
        private bool stageClear2;
        private bool stageClear3;
        private bool stageClear4;
        private bool stageClear5;


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
            Debug.Log(whoisDeadTag);
            if (whoisDeadTag == "Player")
            {
                //if(플레이어목숨수 <0)
                //{
                Debug.Log("실패");
                //_stageIndex = 0;
                //LoadScene();
                //}

            }
            else if (whoisDeadTag == "Monster")
            {
                Debug.Log(monsterCount);
                monsterCount -= 1;
                Debug.Log(monsterCount);
                if (monsterCount <= 0)
                {
                    Debug.Log("클리어");
                    //switch (_stageIndex)
                    //{
                    //    case 1:
                    //        stageClear1 = true; break;
                    //    case 2:
                    //        stageClear2 = true; break;
                    //    case 3:
                    //        stageClear3 = true; break;
                    //    case 4:
                    //        stageClear4 = true; break;
                    //    case 5:
                    //        stageClear5 = true; break;
                    //}
                    //_stageIndex = 0;
                    //LoadScene();

                }
            }
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