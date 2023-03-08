using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject[] arrowsSprites;

    public Button continueButton;

    //Animators
    public Animator OptionsPanelAnimator;

    //Panels
    public GameObject SpacePanel;

    //Scripts:
    private SceneFlow SceneFlowScript;

    private void Awake()
    {
        SceneFlowScript = FindObjectOfType<SceneFlow>(); //We save in this var the ScenFlow that will allow us travel between scenes from the menu
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

    public void NextAutoselect(Button button) //To let the player navigate through the menu with the arrows when we change of panel we select the first selected button
    {
        button.Select();
    }

    #region SelectionArrows
    public void ShowSelectArrow(GameObject arrowSprite)
    {
        foreach (GameObject a in arrowsSprites) { a.SetActive(false); }
        arrowSprite.SetActive(true);
    }

    public void HideSelectArrow(GameObject arrowSprite)
    {
        foreach (GameObject a in arrowsSprites) { a.SetActive(false); }
    }

    #endregion

    #region MenuButtons

    public void ExitGame()
    {
        Application.Quit(); //Exit the aplication, only in the Build
    }
    public void OpenOptionPanel(GameObject panel)
    {
        panel.SetActive(true); //Activate the Option Panel
        OptionsPanelAnimator.SetBool("isActivated", true); //Activates the animation of opening the panel
    }

    public void backButton(GameObject closedPanel)
    {

        OptionsPanelAnimator.SetBool("isActivated", false); //Activated the animation of closing the panel
        DataPersistance.SaveForFutureGames(); //We save, just in case the user settings even if the slider save it each time has a value changed
    }

    public void NewGameButton()
    {
        NewGamePersistance();
        SpacePanel.SetActive(true);
        SpacePanel.GetComponent<Animator>().SetBool("active", true);
        StartCoroutine(SceneFlowScript.GoToScene("NewGame", 1.2f));
    }

    public void ContinueButton()
    {
        SpacePanel.SetActive(true);
        SpacePanel.GetComponent<Animator>().SetBool("active", true);
        SceneFlowScript.GoToGame();
    }

    #endregion

   
     public void LoadUserSettings() //When we arrive to the mainMenu the autosafe options we mark in the previous game is On.
    {
        DataPersistance.hasPlayed = PlayerPrefs.GetInt("Has_Played", 0);

        #region Items & Inventory
        DataPersistance.item1 = PlayerPrefs.GetInt("Item1", 0);
        DataPersistance.item2 = PlayerPrefs.GetInt("Item2", 0);
        DataPersistance.item3 = PlayerPrefs.GetInt("Item3", 0);
        DataPersistance.item4 = PlayerPrefs.GetInt("Item4", 0);

        DataPersistance.inventory1 = PlayerPrefs.GetInt("Inventory1", 0);
        DataPersistance.inventory2 = PlayerPrefs.GetInt("Inventory2", 0);
        DataPersistance.inventory3 = PlayerPrefs.GetInt("Inventory3", 0);
        DataPersistance.inventory4 = PlayerPrefs.GetInt("Inventory4", 0);
        DataPersistance.inventory5 = PlayerPrefs.GetInt("Inventory5", 0);
        #endregion

        #region pieces obtained
        DataPersistance.piece1 = PlayerPrefs.GetInt("Piece1", 0);
        DataPersistance.piece2 = PlayerPrefs.GetInt("Piece2", 0);
        DataPersistance.piece3 = PlayerPrefs.GetInt("Piece3", 0);
        DataPersistance.piece4 = PlayerPrefs.GetInt("Piece4", 0);
        DataPersistance.piecesRemain = PlayerPrefs.GetInt("PiecesRemain", 4);
        #endregion

        #region Doors Openned
        DataPersistance.door1 = PlayerPrefs.GetInt("Door1", 0);
        DataPersistance.door2 = PlayerPrefs.GetInt("Door2", 0);
        DataPersistance.door3 = PlayerPrefs.GetInt("Door3", 0);
        DataPersistance.door4 = PlayerPrefs.GetInt("Door4", 0);
        DataPersistance.door5 = PlayerPrefs.GetInt("Door5", 0);
        DataPersistance.door6 = PlayerPrefs.GetInt("Door6", 0);
        #endregion

        #region DialoguesDone
        DataPersistance.Dialogue1Done = PlayerPrefs.GetInt("Dialogue1Done", 0);
        DataPersistance.Dialogue3Done = PlayerPrefs.GetInt("Dialogue3Done", 0);
        DataPersistance.DialoguePiecesDone = PlayerPrefs.GetInt("PiecesDialogueDone", 0);
        #endregion

        #region Player Last saved Position
        DataPersistance.playerXPos = PlayerPrefs.GetFloat("PlayerXPos", 0);
        DataPersistance.playerYPos = PlayerPrefs.GetFloat("PlayerYPos", 0);
        #endregion

    }

    public void NewGamePersistance()
    {
        //When we hit the new Game Button all the data of data persistence must be reset, to become a normal game,

        DataPersistance.hasPlayed = PlayerPrefs.GetInt("Has_Played", 0);

        #region Items & inventory
        DataPersistance.inventory1 = 0;
        DataPersistance.inventory2 = 0;
        DataPersistance.inventory3 = 0;
        DataPersistance.inventory4 = 0;
        DataPersistance.inventory5 = 0;

        DataPersistance.item1 = 0;
        DataPersistance.item2 = 0;
        DataPersistance.item3 = 0;
        DataPersistance.item4 = 0;
        #endregion

        #region  PiecesObtained
        DataPersistance.piece1 = 0;
        DataPersistance.piece2 = 0;
        DataPersistance.piece3 = 0;
        DataPersistance.piece4 = 0;
        DataPersistance.piecesRemain = 4;
        #endregion

        #region doors
        DataPersistance.door1 = 0;
        DataPersistance.door2 = 0;
        DataPersistance.door3 = 0;
        DataPersistance.door4 = 0;
        DataPersistance.door5 = 0;
        DataPersistance.door6 = 0;
        #endregion

        #region dialoguesDone
        DataPersistance.Dialogue1Done = 0;
        DataPersistance.Dialogue3Done = 0;
        DataPersistance.DialoguePiecesDone = 0;
        #endregion

        #region Player Last saved position
        DataPersistance.playerXPos = 0f;
        DataPersistance.playerYPos = 0;
        #endregion
    }



    IEnumerator WaitAndOff(float timeToWait, GameObject s)
    {
        yield return new WaitForSeconds(timeToWait);
        s.SetActive(false);
    }


}
