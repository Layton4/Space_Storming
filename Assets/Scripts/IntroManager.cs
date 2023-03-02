using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public Image[] objectsOnScreen;

    public GameObject textBox;

    public Button nextButton;
    public Button skipButton;
    public TextMeshProUGUI DialogueText;

    private string OriginalMessage;
    private bool DialogueAnimDone;

    public string[] texts;
    public int currentDialogue;

    public Animator spaceShipAnimator;
    private void Awake()
    {
        OriginalMessage = texts[currentDialogue];
        DialogueText.text = "";
    }

    private void Start()
    {
        StartCoroutine(FadeIn(1.5f));
    }

    IEnumerator FadeIn(float delayToApear)
    {
        Color Color = objectsOnScreen[0].color;

        float AlphaValue = 0;
        Color.a = AlphaValue;

        objectsOnScreen[0].color = Color;

        yield return new WaitForSeconds(delayToApear);
        
        while (AlphaValue <= 1)
        {
            Color.a = AlphaValue;


            objectsOnScreen[0].color = Color;

            AlphaValue += 0.1f;
            yield return new WaitForSeconds(0.075f);
        }
        nextButton.gameObject.SetActive(true);
        nextButton.Select();
        StartCoroutine(Letters(nextButton));
    }

    IEnumerator FadeOut(float delayToDisapear)
    {
        Color Color = objectsOnScreen[0].color;

        float AlphaValue = 1;
        Color.a = AlphaValue;

        objectsOnScreen[0].color = Color;


        yield return new WaitForSeconds(delayToDisapear);

        while (AlphaValue > 0)
        {
            Color.a = AlphaValue;


            objectsOnScreen[0].color = Color;

            AlphaValue -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void NextButtonAnimation(bool value)
    {
        nextButton.gameObject.GetComponent<Animator>().SetBool("Selected", value);
    }
    private IEnumerator Letters(Button selection)
    {
        DialogueAnimDone = false;

        DialogueText.text = "";

        foreach (var d in OriginalMessage) //var (comodín)
        {
            DialogueText.text += d;
            yield return new WaitForSeconds(0.05f);
        }

        DialogueAnimDone = true;
        //selection.gameObject.SetActive(true);
        //selection.Select();
    }

    public void NextButton() 
    {
        if(DialogueAnimDone)
        {
            if (currentDialogue < texts.Length - 1) //If there are more dialogues to read it pass to the next, and hide the next button again
            {
                currentDialogue++;
                OriginalMessage = texts[currentDialogue];
                StartCoroutine(Letters(nextButton));
            }
            else //If it was the last dialogue showed we hide the dialogue box, the text and the button to clean the screen
            {
                DialogueText.text = "";
                StartCoroutine(FadeOut(0.2f));
                spaceShipAnimator.SetBool("Out", true);
                nextButton.gameObject.SetActive(false);
            }
        }
        else
        {
            DialogueAnimDone = true;
            StopAllCoroutines();
            DialogueText.text = texts[currentDialogue];
        }
        
        //nextButton.gameObject.SetActive(false);
        
    }

}
