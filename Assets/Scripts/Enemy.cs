using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool left;
    private bool isDead;
    private Animator _enemyAnimator;
    private bool isAttacking;
    private bool isMoving;

    private PlayerControler playerControllerScript;

    private Collider2D[] results;
    private Collider2D SnakeTrigger;

    [SerializeField] private LayerMask PlayerLayer;
    public float rayExtension = 1.6f;
    private float speed = 2.8f;

    private GameManager gameManagerScript;


    private void Awake()
    {
        gameManagerScript = FindObjectOfType<GameManager>();

        SnakeTrigger = GetComponent<BoxCollider2D>();
        playerControllerScript = FindObjectOfType<PlayerControler>();
        _enemyAnimator = GetComponent<Animator>();

        _enemyAnimator.SetBool("Left", left);

        results = new Collider2D[1];
    }

    private void Update()
    {
        CheckCollision();

        if(transform.position.y > playerControllerScript.gameObject.transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        if(isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

      
    }

    private void LateUpdate()
    {
        _enemyAnimator.SetBool("isDead", isDead);
        _enemyAnimator.SetBool("isAttacking", isAttacking);
        _enemyAnimator.SetBool("isMoving", isMoving);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Bullet"))
        {
            isDead = true;
            otherCollider.gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            Destroy(gameObject, 1.1f);
        }

        if (otherCollider.gameObject.CompareTag("Player"))
        {
            otherCollider.gameObject.GetComponent<PlayerControler>().canMove = false;
            isMoving = false;
            isAttacking = true;
            otherCollider.enabled = false;

            StartCoroutine(killPlayer());
        }

    }

    private void CheckCollision()
    {
        Vector2 rayDirection = Vector2.right;

        if (left)
        {
            rayDirection = Vector2.left;
        }

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, rayDirection, rayExtension, PlayerLayer);



        Color rayColor;

        if (raycastHit.collider != null && raycastHit.distance > 0.1)
        {
            
            rayColor = Color.green;
            isMoving = true;
        }

        else
        {
            isMoving = false;
            rayColor = Color.red;
        }

        
            Debug.DrawRay(transform.position, rayDirection * rayExtension, rayColor);
       
    }
    IEnumerator killPlayer()
    {
        isMoving = false;
        playerControllerScript.canMove = false;
        yield return new WaitForSeconds(0.3f);
        playerControllerScript.isDead = true;

        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
        gameManagerScript.ActivateGameOver();
    }


}
