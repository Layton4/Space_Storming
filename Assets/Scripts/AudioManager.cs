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

    private void Start()
    {
        LoadVolumeSettings();
    }

    public void LoadVolumeSettings() //When we start to play we want to load the values saved in the last game inside the volume sliders
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_Volume", 0.5f);
        musicSlider.value = PlayerPrefs.GetFloat("Music_Volume", 0.5f);

        sfxToggle.isOn = PlayerPrefs.GetInt("SFX_Toggle", 1) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("Music_Toggle", 1) == 1;
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(mixerMusic, musicSlider.value);
        PlayerPrefs.SetFloat(mixerSFX, sfxSlider.value);
    }


    #region Slider Functions
    public void SetMusicVolume(float value)  //When we change the value of the Music slider this value is sended to the audomixer and saved in Data persistence.
    {
        Mixer.SetFloat(mixerMusic, Mathf.Log10(value) * 20);
        DataPersistance.musicVolume = musicSlider.value;
    }

    public void SetSFXVolume(float value)  //When we change the value of the SFX slider this value is sended to the audomixer and saved in Data persistence.
    {
        Mixer.SetFloat(mixerSFX, Mathf.Log10(value) * 20);
        DataPersistance.sfxVolume= sfxSlider.value;
    }

    #endregion

    public void VolumeToggles()
    {
        DataPersistance.sfxToggle = sfxToggle.isOn ? 1 : 0; //When we change the value of the toggle we pass the boolean value to an int to save it in DataPersistence,
        DataPersistance.musicToggle = musicToggle.isOn ? 1 : 0; //if the toggle is On we save the number 1 and if is off we save 0 in datapersistence.
    }

}
