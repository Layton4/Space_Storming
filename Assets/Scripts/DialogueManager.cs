using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private PlayerControler playerControllerScript;

    public bool isTalking;
    public int CurrentDialogueText;
    public bool DialogueAnimDone = false;

    [SerializeField]private List<string[]> dialogueBlocs = new List<string[]>();
    public int currentDialogueBox;

    private void Awake()
    {
        playerControllerScript = FindObjectOfType<PlayerControler>();

        dialogueBlocs.Add(introductionDialogue);
        dialogueBlocs.Add(radioDialogue);

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
            yield return new WaitForSeconds(0.1f);
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
            DialogueAnimDone = true;
            StopAllCoroutines();
            dialogueTextBox.text = dialogueBlocs[currentDialogueBox][CurrentDialogueText];  
        }
    }

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

}
