using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
   /* public AudioMixer Mixer;

    public const string MusicKey = "MusicVolume";
    public const string SFXKey = "SFXVolume";

    private void Awake()
    {
        LoadVolume();
    }

    void LoadVolume() //volume saved on VolumeSettings
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicKey, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXKey, 0.5f);

        Mixer.SetFloat(MusicKey, Mathf.Log10(musicVolume) * 20);
        Mixer.SetFloat(SFXKey, Mathf.Log10(sfxVolume) * 20);
    }*/
}
