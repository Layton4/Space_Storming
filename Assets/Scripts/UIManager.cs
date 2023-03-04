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
    private void Awake()
    {
        TabletAnimator.gameObject.SetActive(true);

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

        if(Input.GetKeyDown(KeyCode.I))
        {
            foreach (GameObject i in panels) { i.SetActive(false); }
            panels[1].SetActive(true);
        }
    }

    public void BackButton()
    {
        TabletAnimator.SetBool("Options", false);
    }

    public void GoMenuButton()
    {
        TabletAnimator.SetBool("GoMenu", true);
        StartCoroutine(sceneFlowScript.GoToScene("Menu", 1.1f));
    }
    IEnumerator OpenOptions()
    {
        TabletAnimator.SetBool("Tablet", true);
        yield return new WaitForSeconds(0.6f);
        TabletAnimator.SetBool("Options", true);
    }

    IEnumerator CloseOptions()
    {
        TabletAnimator.SetBool("Options", false);
        yield return new WaitForSeconds(0.6f);
        TabletAnimator.SetBool("Tablet", false);
    }
}
