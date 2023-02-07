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
    public void volumeChange()
    {
        DataPersistance.sfxVolume = sfxSlider.value;
        DataPersistance.musicVolume = musicSlider.value;

        DataPersistance.SaveForFutureGames();
    }

    public void volumeToggles()
    {
        DataPersistance.sfxToggle = sfxToggle ? 1 : 0;
        DataPersistance.musicToggle = musicToggle ? 1 : 0;

        DataPersistance.SaveForFutureGames();
    }
    #endregion

    private void Start()
    {
        FirstSetVolume();
    }
    public void FirstSetVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_Volume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("Music_Volume", 1f);

        sfxToggle.isOn = PlayerPrefs.GetInt("SFX_Volume", 1) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("Music_Volume", 1) == 1;
    }
    public void LoadVolumeSettings()
    {
        Debug.Log($"AUDIOMANAGER music: {DataPersistance.musicVolume} \n autosave: {DataPersistance.autosaveToggle} sfx: {DataPersistance.sfxVolume}");

        DataPersistance.sfxVolume = PlayerPrefs.GetFloat("SFX_Volume",1f);
        DataPersistance.musicVolume = PlayerPrefs.GetFloat("Music_Volume", 1f);

        DataPersistance.sfxToggle = PlayerPrefs.GetInt("SFX_Volume", 1);
        DataPersistance.musicToggle = PlayerPrefs.GetInt("Music_Volume", 1);

        //Once we have the data of playerprefs introduced into the DataPersistance vars we pass the data into the options.
    }

}
