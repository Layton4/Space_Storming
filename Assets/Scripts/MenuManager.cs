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

    public Toggle[] autosafeToggle;

    public Button continueButton;

    public GameObject SpacePanel;

    private SceneFlow SceneFlowScript;

    private void Awake()
    {
        SceneFlowScript = FindObjectOfType<SceneFlow>();

        foreach (GameObject s in panels) { s.SetActive(false); } //Turn off all the panels
        panels[0].SetActive(true); //Active only the first panel, the MainMenuPanel
    }
    private void Start()
    {
        LoadUserSettings();

        if (DataPersistance.hasPlayed == 0)
        {
            continueButton.interactable = false;
        }

        else
        {
            continueButton.interactable = true;
        }
    }

    public void NextAutoselect(Button button)
    {
        button.Select();
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
        StartCoroutine(WaitAndOff(1.3f, closedPanel));
    }

    public void NewGameButton()
    {
        SpacePanel.SetActive(true);
        SpacePanel.GetComponent<Animator>().SetBool("active", true);
        SceneFlowScript.GoToNewGame();
    }
    #endregion

    #region AutosafeConfiguration
    public void AutoSaveOption(bool value) //When the value of the toggle changes we want to safe in datapersistence if the OnToggle is turn on or off.
    {
        DataPersistance.autosaveToggle = value ? 1 : 0;
    }

    public void LoadUserSettings() //When we arrive to the mainMenu the autosafe options we mark in the previous game is On.
    {
        autosafeToggle[PlayerPrefs.GetInt("Autosave_Toggle",1)].isOn = true;
        DataPersistance.hasPlayed = PlayerPrefs.GetInt("Has_Played", 0);
    }
    #endregion

    #region Corroutines
    IEnumerator WaitAndOff(float timeToWait, GameObject s)
    {
        yield return new WaitForSeconds(timeToWait);
        s.SetActive(false);
    }

    #endregion

}
