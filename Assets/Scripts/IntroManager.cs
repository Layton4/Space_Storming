using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Image objectOnScreen;

    public Button nextButton;

    public TextMeshProUGUI DialogueText;

    private string OriginalMessage;
    private bool DialogueAnimDone;

    public string[] texts;
    public int currentDialogue;

    //Animators
    public Animator spaceShipAnimator;

    //Scripts
    private SceneFlow SceneFlowScript;

    private void Awake()
    {
        SceneFlowScript = FindObjectOfType<SceneFlow>();

        OriginalMessage = texts[currentDialogue];
        DialogueText.text = "";
    }

    private void Start()
    {
        StartCoroutine(FadeIn(1.5f));
    }

    #region FadeIn && FadeOut
    IEnumerator FadeIn(float delayToApear)
    {
        Color Color = objectOnScreen.color; //save the color of our gameObject

        float AlphaValue = 0;
        Color.a = AlphaValue; //we make sure the Alpha value is 0 and the object is not seen when we start this funtion

        objectOnScreen.color = Color; //return the saved color to the gameObject

        yield return new WaitForSeconds(delayToApear);
        
        while (AlphaValue <= 1)
        {
            Color.a = AlphaValue;


            objectOnScreen.color = Color;

            AlphaValue += 0.1f;
            yield return new WaitForSeconds(0.075f); //Each 0.075 seconds the alphavalue increased by 0.1 and continue until the value is alpha 1 and is totally visible
        }
        nextButton.gameObject.SetActive(true);
        nextButton.Select();
        StartCoroutine(Letters());
    }

    IEnumerator FadeOut(float delayToDisapear)
    {
        Color Color = objectOnScreen.color;

        float AlphaValue = 1;
        Color.a = AlphaValue;

        objectOnScreen.color = Color;


        yield return new WaitForSeconds(delayToDisapear);

        while (AlphaValue > 0)
        {
            Color.a = AlphaValue;


            objectOnScreen.color = Color;

            AlphaValue -= 0.1f;
            yield return new WaitForSeconds(0.1f); //Each 0.1 seconds the alphavalue decreased by 0.1 and continue until the value is alpha 0 and is totally invisible
        }
    }
    #endregion

    public void NextButtonAnimation(bool value)
    {
        nextButton.gameObject.GetComponent<Animator>().SetBool("Selected", value);
    }
    private IEnumerator Letters()
    {
        DialogueAnimDone = false;

        DialogueText.text = "";

        foreach (var d in OriginalMessage) //var (comodín)
        {
            DialogueText.text += d;
            yield return new WaitForSeconds(0.05f); //each 0.05 seconds is writen a letter of the sentence and is formed letter by letter the entire message
        }

        DialogueAnimDone = true;

    }

    public void NextButton() 
    {
        if(DialogueAnimDone)
        {
            if (currentDialogue < texts.Length - 1) //If there are more dialogues to read it pass to the next line or sentence
            {
                currentDialogue++;
                OriginalMessage = texts[currentDialogue]; //we save the next message we will reproduce letter by letter
                StartCoroutine(Letters());
            }
            else //If it was the last dialogue showed we hide the dialogue box, the text and the button to clean the screen
            {
                DialogueText.text = ""; //We get empty the text box
                StartCoroutine(FadeOut(0.2f)); //we can wait 0,2 seconds before we start to fade out the dialogue box

                spaceShipAnimator.SetBool("Out", true);
                nextButton.gameObject.SetActive(false);

                if(SceneManager.GetActiveScene().name == "NewGame")
                {
                    StartCoroutine(SceneFlowScript.GoToScene("Game", 2f));
                }
                else
                {
                    StartCoroutine(SceneFlowScript.GoToScene("Menu", 2f));
                }
            }
        }
        else
        {
            DialogueAnimDone = true;
            StopAllCoroutines();
            DialogueText.text = texts[currentDialogue]; //If we push the next button before the texted is complete the corroutine stops and wrote the entire sentence inmediately
            //With this is ready to hit the next button again and pass faster to the next message
            
        }
        
    }

}
