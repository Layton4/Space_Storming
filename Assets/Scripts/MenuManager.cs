using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject[] arrowsSprites;
    public bool autosave = true;

    public void ShowSelectArrow(GameObject arrowSprite)
    {
        foreach(GameObject a in arrowsSprites) { a.SetActive(false); }
        arrowSprite.SetActive(true);
    }

    public void HideSelectArrow(GameObject arrowSprite)
    {
        foreach(GameObject a in arrowsSprites) { a.SetActive(false); }
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();

    }

    public void OpenOptionsPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void AutoSaveOption(bool value)
    {
        autosave = value;
    }

}
