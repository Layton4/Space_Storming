using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Animator TabletAnimator;
    private SceneFlow sceneFlowScript;

    public GameObject[] panels;

    public TextMeshProUGUI[] slots;
    public Image ItemImageSlot;

    public string[] ItemsNames;

    public int firstSlotEmpty;
    public int currentelementindx;

    public int[] inventoryInts;
    public List<int> InventoryItemsInts = new List<int>();

    public GameObject decitionPanel;

    private int wantToUse;
    private string itemUsed;

    private int newPiecesFound;

    public int pieces = 3;


    private void Awake()
    {
        TabletAnimator.gameObject.SetActive(true);

        InventoryItemsInts.Add(0);
        InventoryItemsInts.Add(0);
        InventoryItemsInts.Add(0);
        InventoryItemsInts.Add(0);
        InventoryItemsInts.Add(0);
        firstSlotEmpty = InventoryItemsInts.IndexOf(0);

        UpdateInventory();

        

        sceneFlowScript = FindObjectOfType<SceneFlow>();
        TabletAnimator.SetBool("Options", false);
        TabletAnimator.SetBool("GoMenu", false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            foreach(GameObject i in panels) { i.SetActive(false); }
            panels[0].SetActive(true);

            TabletAnimator.SetBool("Options", true);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            foreach (GameObject i in panels) { i.SetActive(false); }
            panels[1].SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {

            if(firstSlotEmpty < slots.Length)
            {
                if(currentelementindx >= 3)
                {
                    currentelementindx = 3;
                }
                InventoryItemsInts[firstSlotEmpty] = currentelementindx;
                UpdateInventory();
                firstSlotEmpty++;
                currentelementindx++;
            }

            else
            {
                Debug.Log("Tienes el inventario Lleno, ve a la base a entregar las piezas que tengas");
            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            TabletAnimator.SetBool("Inventory", true);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            CheckForPieces();
        }

    }

    public void BackButton()
    {
        TabletAnimator.SetBool("Options", false);
        TabletAnimator.SetBool("Inventory", false);
    }

    public void GoMenuButton()
    {
        TabletAnimator.SetBool("GoMenu", true);
        StartCoroutine(sceneFlowScript.GoToScene("Menu", 1.1f));
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Button slotButton = slots[i].gameObject.GetComponentInParent<Button>();
            slotButton.interactable = true;
            slots[i].text = ItemsNames[InventoryItemsInts[i]];

            if(slots[i].text == "")
            {
                slotButton.interactable = false;
            }
        }
    }

    public void SelectInventoryItem(int slotint)
    {
        decitionPanel.SetActive(true);
        decitionPanel.transform.GetChild(0).GetComponent<Button>().Select();
        wantToUse = slotint;
    }

    public void UseYes()
    {
        Debug.Log($"Has usado un {ItemsNames[inventoryInts[wantToUse]]}");
    }

    public void UseNo()
    {
        decitionPanel.SetActive(false);
        slots[wantToUse].gameObject.GetComponentInParent<Button>().Select();
    }

    public void CheckForPieces()
    {
        newPiecesFound = 0;
        while(InventoryItemsInts.Contains(pieces))
        {
            for (int i = 0; i < inventoryInts.Length; i++)
            {
                if (InventoryItemsInts[i] > 2)
                {
                    newPiecesFound++;
                    DataPersistance.piecesRemain--;
                    InventoryItemsInts.RemoveAt(i);
                    InventoryItemsInts.Add(0);
                    break;
                }
            }
        }

        firstSlotEmpty = InventoryItemsInts.IndexOf(0);
        UpdateInventory();
        if(newPiecesFound > 0)
        {
            Debug.Log($"Nice! you found {newPiecesFound} new pieces, we only need {DataPersistance.piecesRemain} more to get out of here!");
        }

        else
        {
            Debug.Log($"Return when you find more pieces acrossthe spaceship, we need {DataPersistance.piecesRemain} more to fix the capsule");
        }
        
    }

}
