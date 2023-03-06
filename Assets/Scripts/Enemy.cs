using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool left;
    private bool isDead;
    private Animator _enemyAnimator;
    private bool isAttacking;

    private PlayerControler playerControllerScript;

    private Collider2D[] results;
    private Collider2D SnakeTrigger;
    private void Awake()
    {

        SnakeTrigger = GetComponent<BoxCollider2D>();
        playerControllerScript = FindObjectOfType<PlayerControler>();
        _enemyAnimator = GetComponent<Animator>();

        _enemyAnimator.SetBool("Left", left);

        results = new Collider2D[1];
    }

    private void LateUpdate()
    {
        _enemyAnimator.SetBool("isDead", isDead);
        _enemyAnimator.SetBool("isAttacking", isAttacking);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Bullet"))
        {
            isDead = true;
            otherCollider.gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Destroy(gameObject, 1.1f);
        }

        if (otherCollider.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            StartCoroutine(killPlayer());
        }

    }

    private void CheckCollision()
    {
        Vector2 size = SnakeTrigger.bounds.size;
        Vector2 offset = SnakeTrigger.bounds.center;

        int amount = Physics2D.OverlapBoxNonAlloc(offset, size, 0f, results); //OverlapBox check what is inside the zone size and save it inside the array results.

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Player"))
            {

            }
        }
    }
    IEnumerator killPlayer()
    {
        playerControllerScript.canMove = false;
        yield return new WaitForSeconds(0.3f);
        playerControllerScript.isDead = true;

        yield return new WaitForSeconds(0.3f);
        isAttacking = false;

    }


}
