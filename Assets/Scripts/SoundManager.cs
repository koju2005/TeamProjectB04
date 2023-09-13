using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [ReadOnly]
    private AudioSource bgm;
    [ReadOnly]
    private float sfxVolume;

    private const string strBGMVolume = "BGMVolume";
    private const string strSFXVolume = "SFXVolume";
    private void Awake()
    {
        bgm = GetComponent<AudioSource>();
        
        if (PlayerPrefs.HasKey(strBGMVolume))
        {
            bgm.volume = PlayerPrefs.GetFloat(strBGMVolume);
        }
        else
        {
            PlayerPrefs.SetFloat(strBGMVolume,1);
        }

        if (PlayerPrefs.HasKey(strSFXVolume))
        {
            sfxVolume = PlayerPrefs.GetFloat(strSFXVolume);
        }
        else
        {
            PlayerPrefs.SetFloat(strSFXVolume,1);
        }
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(strBGMVolume,volume);
        bgm.volume = volume;
        PlayerPrefs.Save();
    }
    
    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(strSFXVolume,volume);
        sfxVolume = volume;
        PlayerPrefs.Save();
    }

    public void PlayBGM(AudioClip clip)
    {
        bgm.Stop();
        bgm.clip = clip;
        bgm.Play();
    }
    
    public void PlaySFX(AudioClip clip,Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip,position,sfxVolume);
    }
}
