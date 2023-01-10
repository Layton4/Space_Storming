using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public Vector3 moveDirection;
    private float speed = 5f;
    private PlayerController playerControllerScript;

    private void Awake()
    {
        playerControllerScript = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D othercollider2D)
    {
        if(othercollider2D.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnColliderEnter2D(Collider2D othercollider2D)
    {
        if (othercollider2D.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
