using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public int itemNumber;
    private UIManager uiManagerScript;
    public string messageItem;

    public GameObject canvasText;
    

    private void Awake()
    {
        uiManagerScript = FindObjectOfType<UIManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Player"))
        {
            uiManagerScript.AddItemToInventory(itemNumber);
            if (uiManagerScript.firstSlotEmpty <= uiManagerScript.slots.Length)
            {
                Instantiate(canvasText, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
    

}
