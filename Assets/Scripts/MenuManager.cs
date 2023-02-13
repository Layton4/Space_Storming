using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject[] arrowsSprites;

    public GameObject[] panels;

    public Animator OptionsPanelAnimator;

    public Toggle autosafeToggle;

    private AudioManager AudioManagerScript;
    private void Awake()
    {
        AudioManagerScript = FindObjectOfType<AudioManager>();
        foreach (GameObject s in panels) { s.SetActive(false); } //Turn off all the panels
        panels[0].SetActive(true); //Active only the first panel, the MainMenuPanel
    }
    private void Start()
    {
        LoadUserSettings();
    }

    #region SelectionArrows
    public void ShowSelectArrow(GameObject arrowSprite)
    {
        foreach(GameObject a in arrowsSprites) { a.SetActive(false); }
        arrowSprite.SetActive(true);
    }

    public void HideSelectArrow(GameObject arrowSprite)
    {
        foreach(GameObject a in arrowsSprites) { a.SetActive(false); }
    }

    #endregion

    #region MenuButtons
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();

    }

    public void OpenOptionPanel(GameObject panel)
    {
        panel.SetActive(true); //Activate the Option Panel
        OptionsPanelAnimator.SetBool("isActivated", true); //Activates the animation of opening the panel
    }

    public void backButton(GameObject closedPanel)
    {
        OptionsPanelAnimator.SetBool("isActivated",false); //Activated the animation of closing the panel
        DataPersistance.SaveForFutureGames();
        StartCoroutine(WaitAndOff(1.15f, closedPanel));
    }
    #endregion

    public void AutoSaveOption(bool value)
    {
        DataPersistance.autosaveToggle = value ? 1 : 0;
        Debug.Log($"Me cambio a {DataPersistance.autosaveToggle == 1}");
    }

    public void LoadUserSettings()
    {
        autosafeToggle.isOn = PlayerPrefs.GetInt("Autosave_Toggle") == 1;
    }

    IEnumerator WaitAndOff(float timeToWait, GameObject s)
    {
        yield return new WaitForSeconds(timeToWait);
        s.SetActive(false);
    }


}
