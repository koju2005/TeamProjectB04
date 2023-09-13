using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.PlayUI
{
    public class OptionUI : MonoBehaviour
    {
        [SerializeField]private Slider BGM;
        [SerializeField]private Slider SFX;

        private SoundManager sm;
        private void Start()
        {
            sm = GameManager.Instance._soundManager;
            BGM.onValueChanged.AddListener(sm.SetBGMVolume);
            SFX.onValueChanged.AddListener(sm.SetSFXVolume);
        }
    }
}