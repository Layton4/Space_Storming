using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    #region OptionsComponents
    public Slider sfxSlider;
    public Slider musicSlider;

    public Toggle sfxToggle;
    public Toggle musicToggle;
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
        DataPersistance.sfxVolume = PlayerPrefs.GetFloat("SFX_Volume",1f);
        DataPersistance.musicVolume = PlayerPrefs.GetFloat("Music_Volume", 1f);

        DataPersistance.sfxToggle = PlayerPrefs.GetInt("SFX_Toggle", 1);
        DataPersistance.musicToggle = PlayerPrefs.GetInt("Music_Toggle", 1);

        //Once we have the data of playerprefs introduced into the DataPersistance vars we pass the data into the options.

        sfxSlider.value = DataPersistance.sfxVolume;
        musicSlider.value = DataPersistance.musicVolume;

        sfxToggle.isOn = DataPersistance.sfxToggle == 1;
        musicToggle.isOn = DataPersistance.musicToggle == 1;
    }

}
