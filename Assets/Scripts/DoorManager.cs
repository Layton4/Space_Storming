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



    private void Awake()
    {
        dialogueManagerScript = FindObjectOfType<DialogueManager>();
        uiManagerScript = FindObjectOfType<UIManager>();
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (itemInInventory)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorAnimator.SetBool("isOpen", true);
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Player"))
        {
            if(needAnItem)
            {
                if(uiManagerScript.InventoryItemsInts.Contains(neededItemID))
                {
                    itemInInventory = true;
                    Debug.Log("Abre inventario");
                }
                else
                {
                    Debug.Log("No tienes el item necesario para abrir esta puerta");
                }
            }

            else
            {
                Debug.Log("Esto me recuerda a un puzzle");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Player"))
        {
            itemInInventory = false;
        }
    }


}
