using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Logic : MonoBehaviour
{
    private PlayerControler playerControllerScript;
    private GameObject indicativeCanvas;

    private bool canTalk;
    public int intDialogue;

    private DialogueManager dialogueManagerScript;

    private void Awake()
    {
        dialogueManagerScript = FindObjectOfType<DialogueManager>();
        playerControllerScript = FindObjectOfType<PlayerControler>();

        indicativeCanvas = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        indicativeCanvas.SetActive(false);
        canTalk = false;
    }

    void Update()
    {
        #region Sprite Order in Layer
        if (transform.position.y > playerControllerScript.gameObject.transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        #endregion

        if(canTalk && Input.GetKeyDown(KeyCode.E))
        {
            indicativeCanvas.SetActive(false);

            if(intDialogue == 0)
            {
                StopAllCoroutines();
                StartCoroutine(dialogueManagerScript.EnemiesDialogue());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Player"))
        {
            indicativeCanvas.SetActive(true);
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            indicativeCanvas.SetActive(false);
            canTalk = false;
        }
    }
}
