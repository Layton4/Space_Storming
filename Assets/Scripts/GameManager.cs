using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<GameObject> bullets = new List<GameObject>();
    public Transform shotPoint;


    public GameObject inventoryPanel;
    private Animator PanelAnimator;

    public bool openPanel;
    public bool closePanel;

    public GameObject optionspanel;
    public Animator optionPanelAnimator;

    private void Awake()
    {
        PanelAnimator = inventoryPanel.GetComponent<Animator>();
    }
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {

            bullets.Add(Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation));
        }

        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
        }
    }

    private void Update()
    {
        if(openPanel)
        {
            ShowInventoryPanel();
        }

        if(Input.GetKeyDown(KeyCode.C) && closePanel == false)
        {
            HidePanel();
        }

        PanelAnimator.SetBool("activate", openPanel);
        PanelAnimator.SetBool("close", closePanel);
    }

    public void ShowInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        PanelAnimator.SetBool("activate", true);
    }

    public void HidePanel()
    {
        openPanel = false;
        closePanel = true;
        Debug.Log("CerrandoPuerta");
    }

    public void HideIventoryPanel()
    {
        Debug.Log("Funciono");
        PanelAnimator.SetBool("activate", false);
        WaitAndOff(0.5f, inventoryPanel);
    }

    public void OpenOptionPanel()
    {
        optionspanel.SetActive(true); //Activate the Option Panel
        optionPanelAnimator.SetBool("isActivated", true); //Activates the animation of opening the panel
    }

    public void backButton(GameObject closedPanel)
    {
        optionPanelAnimator.SetBool("isActivated", false); //Activated the animation of closing the panel
        DataPersistance.SaveForFutureGames();
        StartCoroutine(WaitAndOff(1.15f, closedPanel));
    }
    IEnumerator WaitAndOff(float timeToWait, GameObject s)
    {
        yield return new WaitForSeconds(timeToWait);
        s.SetActive(false);
    }
}
