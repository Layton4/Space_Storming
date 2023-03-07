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

        UpdateInventory();

        sceneFlowScript = FindObjectOfType<SceneFlow>();
        TabletAnimator.SetBool("Options", false);
        TabletAnimator.SetBool("GoMenu", false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject i in panels) { i.SetActive(false); }
            panels[0].SetActive(true);

            TabletAnimator.SetBool("Options", true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (GameObject i in panels) { i.SetActive(false); }
            panels[1].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            TabletAnimator.SetBool("Inventory", true);
            isPaused = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            CheckForPieces();
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
    }

    public void AddItemToInventory(int itemInt)
    {
        if (firstSlotEmpty < slots.Length)
        {
            InventoryItemsInts[firstSlotEmpty] = itemInt;
            UpdateInventory();
            firstSlotEmpty++;
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
            Debug.Log($"Nice! you found {newPiecesFound} new pieces, we only need {DataPersistance.piecesRemain} more to get out of here!");
        }

        else
        {
            Debug.Log($"Return when you find more pieces acrossthe spaceship, we need {DataPersistance.piecesRemain} more to fix the capsule");
        }

    }

    IEnumerator ApearWaitAndOff(GameObject thing, float timer)
    {
        thing.SetActive(true);
        yield return new WaitForSeconds(timer);
        thing.SetActive(false);
    }

}
