using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Animator dialogueBoxAnimator;
    public Image characterSpriteBox;
    public TextMeshProUGUI characterNameBox;
    public TextMeshProUGUI dialogueTextBox;
    public Button nextButton;

    public Sprite[] charactersSprites;
    public string[] characterNames;

    public string[] introductionDialogue;
    public string[] radioDialogue;
    public string[] enemiesDialogue;

    public string[] aboutPiecesDialogue;
    public string[] lookForPiecesDialogue;

    public string[] NotHaveKeycard2;
    public string[] NotHaveKeycard1;
    public string[] NotHaveTheLever;

    public string[] finalDialogueText;


    private PlayerControler playerControllerScript;

    public bool isTalking;
    public int CurrentDialogueText;
    public bool DialogueAnimDone = false;

    [SerializeField]private List<string[]> dialogueBlocs = new List<string[]>();
    public int currentDialogueBox;

    private UIManager uiManagerScript;

    public bool finalDialogue;

    private SceneFlow sceneFlowManager;


    private void Awake()
    {
        sceneFlowManager = FindObjectOfType<SceneFlow>();
        playerControllerScript = FindObjectOfType<PlayerControler>();
        uiManagerScript = FindObjectOfType<UIManager>();

        dialogueBlocs.Add(introductionDialogue);
        dialogueBlocs.Add(radioDialogue);
        dialogueBlocs.Add(enemiesDialogue);
        dialogueBlocs.Add(aboutPiecesDialogue);
        dialogueBlocs.Add(lookForPiecesDialogue);
        dialogueBlocs.Add(NotHaveKeycard1);
        dialogueBlocs.Add(NotHaveKeycard2);
        dialogueBlocs.Add(NotHaveTheLever);
        dialogueBlocs.Add(finalDialogueText);

    }
    void Start()
    {
        if(DataPersistance.Dialogue1Done == 0)
        {
            StartCoroutine(IntroductionDialogue());
        }
    }
   
    IEnumerator Letters()
    {
        
        string originalMessage = dialogueTextBox.text;
        dialogueTextBox.text = "";
        DialogueAnimDone = false;

        foreach (var d in originalMessage) //var (comodín)
        {
            dialogueTextBox.text += d;
            yield return new WaitForSeconds(0.075f);
        }

        DialogueAnimDone = true;
    }

   
    public void NextButton()
    {
        if(DialogueAnimDone)
        {
            CurrentDialogueText++;
            if(CurrentDialogueText < dialogueBlocs[currentDialogueBox].Length)
            {
                dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];
                StartCoroutine(Letters());
            }
            else
            {
                nextButton.gameObject.SetActive(false);
                dialogueBoxAnimator.SetBool("isTalking", false);
                dialogueTextBox.text = "";
                playerControllerScript.canMove = true;

                if(finalDialogue) { StartCoroutine(sceneFlowManager.GoToScene("Credits", 2f));}

                if(currentDialogueBox == 0)
                {
                    DataPersistance.hasPlayed = 1;
                    DataPersistance.Dialogue1Done = 1;
                    DataPersistance.SaveForFutureGames();
                }

            }
        }

        else
        {
            if (currentDialogueBox != 4)
            {
                DialogueAnimDone = true;
                StopAllCoroutines();
                dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];
            }

        }
    }

    #region Dialogues Coroutines
    IEnumerator IntroductionDialogue()
    {
        currentDialogueBox = 0;
        CurrentDialogueText = 0;
        characterSpriteBox.sprite = charactersSprites[0];
        characterNameBox.text = characterNames[0];
        playerControllerScript.canMove = false;

        yield return new WaitForSeconds(0.25f);

        dialogueBoxAnimator.SetBool("isTalking", true);

        yield return new WaitForSeconds(1.1f);

        nextButton.gameObject.SetActive(true);

        dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];
        StartCoroutine(Letters());

    }


    public IEnumerator PickRadioDialogue()
    {

        currentDialogueBox = 1;
        CurrentDialogueText = 0;
        characterSpriteBox.sprite = charactersSprites[0];
        characterNameBox.text = characterNames[0];
        playerControllerScript.canMove = false;

        dialogueBoxAnimator.SetBool("isTalking", true);

        yield return new WaitForSeconds(1.1f);

        nextButton.gameObject.SetActive(true);

        dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];
        StartCoroutine(Letters());

    }

    public IEnumerator EnemiesDialogue()
    {
        currentDialogueBox = 2;
        CurrentDialogueText = 0;
        characterSpriteBox.sprite = charactersSprites[1];
        characterNameBox.text = characterNames[1];
        playerControllerScript.canMove = false;
        dialogueBoxAnimator.SetBool("isTalking", true);

        yield return new WaitForSeconds(1.1f);

        nextButton.gameObject.SetActive(true);
        dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];
        StartCoroutine(Letters());

    }

    public IEnumerator AboutThePieces()
    {
        currentDialogueBox = 3;
        CurrentDialogueText = 0;
        characterSpriteBox.sprite = charactersSprites[1];
        characterNameBox.text = characterNames[1];
        playerControllerScript.canMove = false;
        dialogueBoxAnimator.SetBool("isTalking", true);

        yield return new WaitForSeconds(1.1f);

        nextButton.gameObject.SetActive(true);
        dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];
        StartCoroutine(Letters());
        DataPersistance.DialoguePiecesDone = 1;
        DataPersistance.SaveForFutureGames();
    }
    public IEnumerator CheckPiecesDialogue()
    {
        dialogueTextBox.text = "";
        currentDialogueBox = 4;
        CurrentDialogueText = 0;
        characterSpriteBox.sprite = charactersSprites[1];
        characterNameBox.text = characterNames[1];
        playerControllerScript.canMove = false;
        dialogueBoxAnimator.SetBool("isTalking", true);
        yield return new WaitForSeconds(1.1f);

        nextButton.gameObject.SetActive(true);
        uiManagerScript.CheckForPieces();
        StartCoroutine(Letters());

    }

   public IEnumerator DoorClosedDialogue(int i)
    {
        dialogueTextBox.text = "";

        CurrentDialogueText = 0;
        if(i == 1)
        {
            currentDialogueBox = 7;
        }

        else
        {
            currentDialogueBox = i + 2;
        }

        characterSpriteBox.sprite = charactersSprites[0];
        characterNameBox.text = characterNames[0];
        playerControllerScript.canMove = false;
        dialogueBoxAnimator.SetBool("isTalking", true);

        yield return new WaitForSeconds(1.1f);

        nextButton.gameObject.SetActive(true);
        dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];
        StartCoroutine(Letters());
    }

    public IEnumerator FinalDialogue()
    {
        dialogueTextBox.text = "";
        currentDialogueBox = 8;
        CurrentDialogueText = 0;

        characterSpriteBox.sprite = charactersSprites[1];
        characterNameBox.text = characterNames[1];
        playerControllerScript.canMove = false;
        dialogueBoxAnimator.SetBool("isTalking", true);

        yield return new WaitForSeconds(1.1f);

        nextButton.gameObject.SetActive(true);
        dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];
        StartCoroutine(Letters());

        finalDialogue = true;

    }
    #endregion
}
