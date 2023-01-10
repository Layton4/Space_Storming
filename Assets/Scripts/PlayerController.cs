using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.75f;
    public const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";
    public const string LASTH = "LastHorizontal", LASTV = "LastVertical";

    public float xInput, yInput;
    private float inputTol = 0.2f;

    private Rigidbody2D playerRigidbody;
    [SerializeField]private Vector2 moveDirection;
    public Vector2 lastDirection;

    private Animator playerAnimator;
    private GameManager gameManagerScript;

    public GameObject bulletPrefab;
    private Transform shotPoint;

    public List<GameObject> bullets;
    public int currentBullet;
    private bool isWalking;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        shotPoint = transform.GetChild(0).GetComponent<Transform>();
        gameManagerScript = FindObjectOfType<GameManager>();

        bullets = gameManagerScript.bullets;
    }

    private void Start()
    {
        lastDirection = Vector2.up;
    }
    private void Update()
    {
        isWalking = false;

        Movement();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            bullets[currentBullet].transform.position = lastDirection;
            bullets[currentBullet].SetActive(true);
            
            currentBullet++;
            if(currentBullet >= bullets.Count)
            {
                currentBullet = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        playerAnimator.SetBool("isWalking", isWalking);
        playerAnimator.SetFloat(HORIZONTAL, xInput);
        playerAnimator.SetFloat(VERTICAL, yInput);

        playerAnimator.SetFloat(LASTH, lastDirection.x);
        playerAnimator.SetFloat(LASTV, lastDirection.y);

    }

    private void Movement()
    {
        xInput = Input.GetAxisRaw(HORIZONTAL);
        yInput = Input.GetAxisRaw(VERTICAL);

        if (Mathf.Abs(xInput) > inputTol)
        {
            isWalking = true;
            lastDirection = new Vector2(xInput, 0);
        }

        if (Mathf.Abs(yInput) > inputTol)
        {
            isWalking = true;
            lastDirection = new Vector2(0, yInput);
        }

        moveDirection = new Vector2(xInput, yInput).normalized * speed;
    }
}
