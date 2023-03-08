using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

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

    public bool isPaused;

    private GameObject EventSyst;

    public GameObject filledInventoryText;

    public List<int> inventoryPersistance = new List<int>();

    public GameObject[] importantPiece;

    public List<int> pickedPieces = new List<int>();

    public List<int> pickedItems = new List<int>();

    private int firstPieceInt = 5;
    private int lastPieceInt = 8;

    public GameObject[] ItemsInMap;

    private GameManager gameManagerScript;

    public TextMeshProUGUI dialogueTextBox;

    private void Awake()
    {
        EventSyst = GameObject.Find("EventSystem");

        filledInventoryText.SetActive(false);
        TabletAnimator.gameObject.SetActive(true);

        InventoryItemsInts.Add(0);
        InventoryItemsInts.Add(0);
        InventoryItemsInts.Add(0);
        InventoryItemsInts.Add(0);
        InventoryItemsInts.Add(0);

        firstSlotEmpty = InventoryItemsInts.IndexOf(0);
        sceneFlowScript = FindObjectOfType<SceneFlow>();
        TabletAnimator.SetBool("Options", false);
        TabletAnimator.SetBool("GoMenu", false);

        pickedPieces.Add(DataPersistance.piece1);
        pickedPieces.Add(DataPersistance.piece2);
        pickedPieces.Add(DataPersistance.piece3);
        pickedPieces.Add(DataPersistance.piece4);

        pickedItems.Add(DataPersistance.item1);
        pickedItems.Add(DataPersistance.item2);
        pickedItems.Add(DataPersistance.item3);
        pickedItems.Add(DataPersistance.item4);

        gameManagerScript = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        LoadInventory();
        UpdateInventory();
        HidePickedPieces();
        HidePickedItems();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject i in panels) { i.SetActive(false); }
            panels[0].SetActive(true);

            TabletAnimator.SetBool("Options", true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            TabletAnimator.SetBool("Inventory", true);
            isPaused = true;
        }

    }

    public void BackButton()
    {
        TabletAnimator.SetBool("Options", false);
        TabletAnimator.SetBool("Inventory", false);
        decitionPanel.SetActive(false);
        isPaused = false;

        EventSyst.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

    }

    public void GoMenuButton()
    {
        TabletAnimator.SetBool("GoMenu", true);

        gameManagerScript.SafeLastPosition();
        DataPersistance.SaveForFutureGames();

        StartCoroutine(sceneFlowScript.GoToScene("Menu", 1.1f));
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Button slotButton = slots[i].gameObject.GetComponentInParent<Button>();
            slotButton.interactable = true;
            slots[i].text = ItemsNames[InventoryItemsInts[i]];

            if (slots[i].text == "")
            {
                slotButton.interactable = false;
            }
        }
        SaveInventory();

    }

    public void AddItemToInventory(int itemInt)
    {
        if (firstSlotEmpty < slots.Length)
        {
            InventoryItemsInts[firstSlotEmpty] = itemInt;
            UpdateInventory();
            firstSlotEmpty++;

            if(itemInt >= firstPieceInt && itemInt <= lastPieceInt)
            {
                int convertedint = itemInt - 5;
                pickedPieces[convertedint] = 1;
                SavePickedPieces();
            }

            else
            {
                int convertedint = itemInt - 1;
                pickedItems[convertedint] = 1;
                SavePickedItems();
            }

            gameManagerScript.SafeLastPosition();
        }

        else
        {
            firstSlotEmpty = 6;
            StartCoroutine(ApearWaitAndOff(filledInventoryText, 2f));
            Debug.Log("Tienes el inventario Lleno, ve a la base a entregar las piezas que tengas bro!");
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
        StopAllCoroutines();
        dialogueTextBox.text = "";
        newPiecesFound = 0;

        for(int i = 5; i <= 8; i++)
        {
            if(InventoryItemsInts.Contains(i))
            {
                newPiecesFound++;
                DataPersistance.piecesRemain--;
                InventoryItemsInts.Remove(i);
                InventoryItemsInts.Add(0);
            }
        }
        

        firstSlotEmpty = InventoryItemsInts.IndexOf(0);
        UpdateInventory();
        if (newPiecesFound > 0)
        {
            dialogueTextBox.text = $"Nice! you found {newPiecesFound} new pieces, we only need {DataPersistance.piecesRemain} more to get out of here!";
            //Debug.Log($"Nice! you found {newPiecesFound} new pieces, we only need {DataPersistance.piecesRemain} more to get out of here!");
        }

        else
        {
            dialogueTextBox.text = $"Return when you find more pieces acrossthe spaceship, we need {DataPersistance.piecesRemain} more to fix the capsule";
            //Debug.Log($"Return when you find more pieces acrossthe spaceship, we need {DataPersistance.piecesRemain} more to fix the capsule");
        }

    }

    public void RespawnButton()
    {
        sceneFlowScript.GoToGame();
    }
    IEnumerator ApearWaitAndOff(GameObject thing, float timer)
    {
        thing.SetActive(true);
        yield return new WaitForSeconds(timer);
        thing.SetActive(false);
    }

    public void SaveInventory()
    {
        DataPersistance.inventory1 = InventoryItemsInts[0];
        DataPersistance.inventory2 = InventoryItemsInts[1];
        DataPersistance.inventory3 = InventoryItemsInts[2];
        DataPersistance.inventory4 = InventoryItemsInts[3];
        DataPersistance.inventory5 = InventoryItemsInts[4];

        DataPersistance.SaveForFutureGames();
    }

    public void LoadInventory()
    {
        InventoryItemsInts[0] = DataPersistance.inventory1;
        InventoryItemsInts[1] = DataPersistance.inventory2;
        InventoryItemsInts[2] = DataPersistance.inventory3;
        InventoryItemsInts[3] = DataPersistance.inventory4;
        InventoryItemsInts[4] = DataPersistance.inventory5;
    }

    public void HidePickedPieces()
    {
        for(int i = 0; i<importantPiece.Length; i++)
        {
            importantPiece[i].SetActive(pickedPieces[i] == 0);
        }
    }

    public void HidePickedItems()
    {
        for(int i = 0; i < ItemsInMap.Length; i++)
        {
            ItemsInMap[i].SetActive(pickedItems[i] == 0);
        }
    }
    public void SavePickedPieces()
    {
        DataPersistance.piece1 = pickedPieces[0];
        DataPersistance.piece2 = pickedPieces[1];
        DataPersistance.piece3 = pickedPieces[2];
        DataPersistance.piece4 = pickedPieces[3];

        DataPersistance.SaveForFutureGames();
    }

    public void SavePickedItems()
    {
        DataPersistance.item1 = pickedItems[0];
        DataPersistance.item2 = pickedItems[1];
        DataPersistance.item3 = pickedItems[2];
        DataPersistance.item4 = pickedItems[3];

        DataPersistance.SaveForFutureGames();
    }

}
