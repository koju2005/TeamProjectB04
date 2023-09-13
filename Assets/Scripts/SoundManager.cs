using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource ui;
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
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(strBGMVolume,volume);
        bgm.volume = volume;
        PlayerPrefs.Save();
    }

    public float GetBGMVolume()
    {
        return bgm.volume;
    }
    
    public float GetSFXVolume()
    {
        return sfxVolume;
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

    public void PlayUI(AudioClip clip)
    {
        ui.Stop();
        ui.clip = clip;
        ui.volume = sfxVolume;
        ui.Play();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }
    
    public void PlaySFX(AudioClip clip,Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip,position,sfxVolume);
    }
}
