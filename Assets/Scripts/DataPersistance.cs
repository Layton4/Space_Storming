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

    public static int inventory1;
    public static int inventory2;
    public static int inventory3;
    public static int inventory4;
    public static int inventory5;

    public static int piece1;
    public static int piece2;
    public static int piece3;
    public static int piece4;

    public static int item1;
    public static int item2;
    public static int item3;
    public static int item4;

    public static float playerXPos;
    public static float playerYPos;


    public static int Dialogue1Done;
    public static int Dialogue3Done;
    public static int DialoguePiecesDone;

    public static int door1;
    public static int door2;
    public static int door3;
    public static int door4;
    public static int door5;
    public static int door6;

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
        PlayerPrefs.SetInt("Inventory1", inventory1);
        PlayerPrefs.SetInt("Inventory2", inventory2);
        PlayerPrefs.SetInt("Inventory3", inventory3);
        PlayerPrefs.SetInt("Inventory4", inventory4);
        PlayerPrefs.SetInt("Inventory5", inventory5);

        PlayerPrefs.SetInt("Piece1", piece1);
        PlayerPrefs.SetInt("Piece2", piece2);
        PlayerPrefs.SetInt("Piece3", piece3);
        PlayerPrefs.SetInt("Piece4", piece4);

        PlayerPrefs.SetInt("Item1", item1);
        PlayerPrefs.SetInt("Item2", item2);
        PlayerPrefs.SetInt("Item3", item3);
        PlayerPrefs.SetInt("Item4", item4);

        PlayerPrefs.SetInt("Dialogue1Done", Dialogue1Done);
        PlayerPrefs.SetInt("Dialogue3Done", Dialogue3Done);

        PlayerPrefs.SetFloat("PlayerXPos", playerXPos);

        PlayerPrefs.SetFloat("PlayerYPos", playerYPos);

        PlayerPrefs.SetFloat("PiecesDialogueDone", DialoguePiecesDone);

        PlayerPrefs.SetInt("Door1", door1);
        PlayerPrefs.SetInt("Door2", door2);
        PlayerPrefs.SetInt("Door3", door3);
        PlayerPrefs.SetInt("Door4", door4);
        PlayerPrefs.SetInt("Door5", door5);
        PlayerPrefs.SetInt("Door6", door6);

    }

}
