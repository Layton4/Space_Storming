using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class AudioManager : MonoBehaviour
{
    #region OptionsComponents
    public Slider sfxSlider;
    public Slider musicSlider;

    public Toggle sfxToggle;
    public Toggle musicToggle;

    public AudioMixer Mixer;

    public const string mixerMusic = "MusicVolume";
    public const string mixerSFX = "SFXVolume";
    #endregion


    #region OptionsVolumeChanges
    public void VolumeChange()
    {
        DataPersistance.sfxVolume = sfxSlider.value;
        DataPersistance.musicVolume = musicSlider.value;
    }

    public void VolumeToggles()
    {
        DataPersistance.sfxToggle = sfxToggle.isOn ? 1 : 0;
        DataPersistance.musicToggle = musicToggle.isOn ? 1 : 0;
    }
    #endregion

    private void Start()
    {
        LoadVolumeSettings();
    }

    public void LoadVolumeSettings()
    {
        DataPersistance.sfxVolume = PlayerPrefs.GetFloat("SFX_Volume",0.5f);
        DataPersistance.musicVolume = PlayerPrefs.GetFloat("Music_Volume", 0.5f);

        DataPersistance.sfxToggle = PlayerPrefs.GetInt("SFX_Toggle", 1);
        DataPersistance.musicToggle = PlayerPrefs.GetInt("Music_Toggle", 1);

        //Once we have the data of playerprefs introduced into the DataPersistance vars we pass the data into the options.

        sfxSlider.value = DataPersistance.sfxVolume;
        musicSlider.value = DataPersistance.musicVolume;

        sfxToggle.isOn = DataPersistance.sfxToggle == 1;
        musicToggle.isOn = DataPersistance.musicToggle == 1;
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.mixerMusic, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.mixerSFX, sfxSlider.value);
    }

    public void SetMusicVolume(float value)
    {
        Mixer.SetFloat(mixerMusic, Mathf.Log10(value) * 20);
        DataPersistance.musicVolume = musicSlider.value;
    }

    public void SetSFXVolume(float value)
    {
        Mixer.SetFloat(mixerSFX, Mathf.Log10(value) * 20);
        DataPersistance.sfxVolume= sfxSlider.value;
    }
}
