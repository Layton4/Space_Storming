using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public ParticleSystem bulletParticles;
    private Vector3 offsetAngle = new Vector3(0, 90, 0);
    private float speed = 14f;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D othercollider2D)
    {
        if(othercollider2D.gameObject.CompareTag("Wall"))
        {
            Instantiate(bulletParticles, transform.position, bulletParticles.transform.rotation);
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
