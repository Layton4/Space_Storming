using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistance
{
    #region Vars
    //Music and SfX
    public static float sfxVolume;
    public static int sfxToggle;

    public static float musicVolume;
    public static int musicToggle;

    //Autosafe
    public static int autosaveToggle;

    //There is data
    public static int hasPlayed;

    public static int piecesRemain = 4;
    #endregion

    public static void SaveForFutureGames()
    {
        //Volumes Settigns
        PlayerPrefs.SetFloat("SFX_Volume", sfxVolume);
        PlayerPrefs.SetInt("SFX_Toggle", sfxToggle);
        PlayerPrefs.SetFloat("Music_Volume", musicVolume);
        PlayerPrefs.SetInt("Music_Toggle", musicToggle);

        //Autosave Settings
        PlayerPrefs.SetInt("Autosave_Toggle", autosaveToggle);

        //There is data
        PlayerPrefs.SetInt("Has_Played", hasPlayed);

        PlayerPrefs.SetInt("PiecesRemain", piecesRemain);
    }

}
