using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BGMStarter : MonoBehaviour
{
    [SerializeField] private AudioClip startBGM;

    private void Start()
    {
        GameManager.Instance.PlayBGM(startBGM);
    }
}
