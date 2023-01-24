using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] arrowsSprites;
    
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

}
