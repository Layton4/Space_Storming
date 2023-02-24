using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.5f;
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

    public bool canShot;

    public List<GameObject> bullets;
    public int currentBullet;
    private bool isWalking;

    private GameObject messagePanel;
    private Animator DoorAnimator;

    private bool isOnTrigger;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        shotPoint = transform.GetChild(0).GetComponent<Transform>();
        gameManagerScript = FindObjectOfType<GameManager>();


        bullets = gameManagerScript.bullets;
        canShot = true;

        isOnTrigger = false;
    }

    private void Start()
    {
        lastDirection = Vector2.up;
    }
    private void Update()
    {
        isWalking = false;


        Movement();

        if(Input.GetKeyDown(KeyCode.Space) && canShot)
        {

            //Instantiate(bulletPrefab, shotTestObj.transform.position, transform.rotation);
            StartCoroutine(ShotCooldown());
        }

        if(isOnTrigger && Input.GetKeyDown(KeyCode.E))
        {
            messagePanel.SetActive(false);
            Debug.Log("Abriendo Puerta");

            DoorAnimator.SetBool("isOpen", true);

            gameManagerScript.openPanel = true;
            gameManagerScript.closePanel = false;
            //isOnTrigger = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameManagerScript.OpenOptionPanel();
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

    public IEnumerator ShotCooldown()
    {
        float shotTimer = 1f;
        if(canShot == true)
        {

            bullets[currentBullet].SetActive(true);
            bullets[currentBullet].transform.position = shotPoint.position;
            bullets[currentBullet].GetComponent<BulletLogic>().moveDirection = lastDirection;
            currentBullet++;
            if (currentBullet >= bullets.Count)
            {
                currentBullet = 0;
            }
            canShot = false;
        }
        yield return new WaitForSeconds(shotTimer);
        canShot = true;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Door"))
        {
            DoorAnimator = otherCollider.gameObject.GetComponent<Animator>();
            messagePanel = otherCollider.gameObject.transform.GetChild(0).gameObject;
            messagePanel.SetActive(true);
  
            isOnTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
        isOnTrigger = false;
    }

}
