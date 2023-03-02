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
    public void VolumeToggles()
    {
        DataPersistance.sfxToggle = sfxToggle.isOn ? 1 : 0;
        DataPersistance.musicToggle = musicToggle.isOn ? 1 : 0;
    }

    public IEnumerator FadeOutVolume(AudioMixer audioMixer,string parameter, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;

        audioMixer.GetFloat(parameter, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(parameter, Mathf.Log10(newVol) * 20);
            yield return null;
        }

        yield break;
    }
}
