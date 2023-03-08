using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public bool usedItem;
    public int neededItemID;

    public GameObject CanvasText;
    private DialogueManager dialogueManagerScript;


    private void Awake()
    {
        dialogueManagerScript = FindObjectOfType<DialogueManager>();
    }

}
