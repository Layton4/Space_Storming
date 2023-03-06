using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemText : MonoBehaviour
{

    float verticalSpeed = 0.3f;
    float scaleFactor = 10f;

    private TextMeshProUGUI canvatext;

    private void Start()
    {
        canvatext = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        canvatext.text = "New Item";
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + verticalSpeed * Time.deltaTime, 0);
        transform.localScale *= 1 - Time.deltaTime/scaleFactor;
    }
}
