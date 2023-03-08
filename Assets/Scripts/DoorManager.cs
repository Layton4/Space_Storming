using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public bool needAnItem;

    private bool itemInInventory;
    public int neededItemID;

    public GameObject CanvasText;
    private DialogueManager dialogueManagerScript;
    private UIManager uiManagerScript;

    private Animator doorAnimator;

    private GameObject indicativeCanvas;

    
    

    private void Awake()
    {
        indicativeCanvas = gameObject.transform.GetChild(0).gameObject;

        dialogueManagerScript = FindObjectOfType<DialogueManager>();
        uiManagerScript = FindObjectOfType<UIManager>();
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        indicativeCanvas.SetActive(false);
    }
    private void Update()
    {
        if (needAnItem && Input.GetKeyDown(KeyCode.E))
        {
            if (itemInInventory)
            {
                doorAnimator.SetBool("isOpen", true);
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(dialogueManagerScript.DoorClosedDialogue(neededItemID));
            }
        }
  
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Player"))
        {
            needAnItem = true;
            indicativeCanvas.SetActive(true);
           
            if(uiManagerScript.InventoryItemsInts.Contains(neededItemID))
            {
                itemInInventory = true;
            }
            else
            {
                Debug.Log("No tienes el item necesario para abrir esta puerta");
            }
        

        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Player"))
        {
            needAnItem = false;
            itemInInventory = false;
            indicativeCanvas.SetActive(false);
        }
    }


}
