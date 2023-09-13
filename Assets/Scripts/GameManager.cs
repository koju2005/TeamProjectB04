﻿using System;
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
        public SoundManager _soundManager { get; private set; }

        public HashSet<GameObject> _currentWeapons { get; private set; }

        private static GameManager _instance;
        private GameObject _player;
        public static bool isApplicationExit = false;
        public int _stageIndex;
        public int monsterCount=0;
        public int ballCount = 0;
        public bool[] stageClear = new bool[5];



        public event Action win;
        public event Action<string> lose;

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
            _soundManager = GetComponent<SoundManager>();
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
            
            if (whoisDeadTag == "Monster")
            {
                monsterCount-=1;
                if (monsterCount <= 0) 
                {
                    //클리어 창 뜨게 만들기
                    switch(_stageIndex)
                    {
                        case 0:
                            stageClear[0] = true; break;
                        case 1:
                            stageClear[1] = true; break;
                        case 2:
                            stageClear[2] = true; break;
                        case 3:
                            stageClear[3] = true; break;
                        case 4:
                            stageClear[4] = true; break;
                        default:
                            Debug.Log("스테이지 넘버 오류");
                            break;
                    }
                    //Debug.Log("이게1번같은데?");
                    win?.Invoke();
                }
            }
        }

        public void ballcheck()
        {
            if (ballCount == 0) 
            {
                lose?.Invoke("아몰라");
            }
        }

        public void AddWeapon(GameObject weapon)
        {
            _currentWeapons.Add(weapon);
            if (weapon.layer == LayerMask.NameToLayer("UserWeapon"))
            {
                ballCount += 1;
            }
        }

        public void RemoveWeapon(GameObject weapon)
        {
            _currentWeapons.Remove(weapon);
            if (weapon.layer == LayerMask.NameToLayer("UserWeapon"))
            {
                ballCount -= 1;
                if (ballCount <= 0) 
                {
                    Invoke("ballcheck", 1f);

                }
            }
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

        public void LoadSelecteScene()
        {
            Time.timeScale = 1f;
            _stageIndex = 7;
            LoadScene();
        }

        public void PlayBGM(AudioClip clip)
        {
            _soundManager.PlayBGM(clip);
        }
        
        public void PlaySFX(AudioClip clip,Vector3 position)
        {
            _soundManager.PlaySFX(clip,position);
        }
    }
}