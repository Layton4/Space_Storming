using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed = 3f;

    public bool isDead;

    private const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";

    private float xInput, yInput;
    private float inputTol = 0.2f;

    public bool isWalking;
    public bool canMove = true;

    private Rigidbody2D _playerRigidbody;
    public Vector2 lastDirection;
    private Animator _animator;
    public const string LASTH = "LastHorizontal", LASTV = "LastVertical";

    private bool isShooting;
    [SerializeField]private float shotTime;
    private float shotTimeCounter;

    private GameManager gameManagerScript;
    private UIManager uiManagerScript;

    private DialogueManager dialogueManagerScript;
    private void Awake()
    {
        dialogueManagerScript = FindObjectOfType<DialogueManager>();
        uiManagerScript = FindObjectOfType<UIManager>();

        _animator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody2D>();

        gameManagerScript = FindObjectOfType<GameManager>();

        lastDirection = Vector2.right;
    }
    void Update()
    {
        if(uiManagerScript.isPaused == false)
        {
            isWalking = false;
            if (!canMove)
            {
                return;
            }

            yInput = Input.GetAxisRaw(VERTICAL);
            xInput = Input.GetAxisRaw(HORIZONTAL);

            if (isShooting)
            {
                shotTimeCounter -= Time.deltaTime;
                if (shotTimeCounter < 0)
                {
                    isShooting = false;
                }
            }

            else if (Input.GetKeyDown(KeyCode.Space))
            {
                isShooting = true;
                shotTimeCounter = shotTime;
                gameManagerScript.Prepareshot(gameObject.transform.GetChild(0));
            }

            else
            {
                Movement();
            }

        }
        

    }

    private void LateUpdate()
    {
        if(!isWalking || isShooting)
        {
            _playerRigidbody.velocity = Vector2.zero;
        }
            _animator.SetFloat(HORIZONTAL, xInput);
            _animator.SetFloat(VERTICAL, yInput);

            _animator.SetFloat(LASTH, lastDirection.x);
            _animator.SetFloat(LASTV, lastDirection.y);

            _animator.SetBool("isWalking", isWalking);

            _animator.SetBool("isDead", isDead);

            _animator.SetBool("isShooting", isShooting);

        
    }

    public void Movement()
    {
        if (Mathf.Abs(xInput) > inputTol)
        {
            Vector3 translation = new Vector3(xInput * speed * Time.deltaTime, 0, 0);
            _playerRigidbody.velocity = new Vector2(xInput * speed, 0);

            isWalking = true;
            lastDirection = new Vector2(xInput, 0);
        }

        if (Mathf.Abs(yInput) > inputTol)
        {
            Vector3 translation_y = new Vector3(0, yInput * speed * Time.deltaTime, 0);
            _playerRigidbody.velocity = new Vector2(0, yInput * speed);
            lastDirection = new Vector2(yInput, 0);

            isWalking = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("radio"))
        {
            StartCoroutine(dialogueManagerScript.PickRadioDialogue());
        }
    }

}
