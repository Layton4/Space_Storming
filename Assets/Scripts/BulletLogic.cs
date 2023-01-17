using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public Vector3 moveDirection;
    private float speed = 7f;


    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
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
