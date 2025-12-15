using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class BoxingController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    [SerializeField] private float raycastDistance = 2;

    [Header("Rotation Settings")]
    [SerializeField] private int rotateSpeed = 20;

    [Header("References")]
    public LayerMask PlayerLayer;
    public Transform holder;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private PlayerInputManager playerInputManager;
    [SerializeField]
    private GameObject inputManagerHolder;
    public Sprite[] sprites;

    [SerializeField]
    public bool isBlockingLeft, isBlockingRight;
    [SerializeField]
    private Transform RayPoint;

    private BoxingHPManager OtherPlayersHP;
    private string OtherPlayersTag;
    public GameObject OtherPlayer;
    private GameObject RoundManagerHolder;
    private BoxingRoundManager RoundManager;
    private BoxingHPManager playerHPManager;

    private void Start()
    {
        if (playerInput.playerIndex == 0)
        {
            //p1Position = GameObject.FindGameObjectWithTag("p1");
            //transform.position = p1Position.transform.position;
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            //sprite.sprite = sprites[0]; 
            gameObject.tag = "Player1";
            OtherPlayersTag = "Player2";
           
        }
        else if (playerInput.playerIndex == 1)
        {
           // p1Position = GameObject.FindGameObjectWithTag("p2");
           // transform.position = p1Position.transform.position;
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            //sprite.sprite = sprites[1];
            gameObject.tag = "Player2";
            OtherPlayersTag = "Player1";
        }
        inputManagerHolder = GameObject.FindGameObjectWithTag("GameController");
        playerInputManager = inputManagerHolder.GetComponent<PlayerInputManager>();
        RoundManagerHolder = GameObject.FindGameObjectWithTag("PointManager");
        RoundManager = RoundManagerHolder.GetComponent<BoxingRoundManager>();
        playerHPManager = GetComponent<BoxingHPManager>();
    }

    public void OnGameSelection(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("GameSelect");
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


  
    public void OnRightPunch(InputAction.CallbackContext context)
    {
        if (context.performed) // Only trigger on button press, not release
        {
            RightPunch();

        }
    }

    public void OnLeftPunch(InputAction.CallbackContext context)
    {
        if (context.performed) // Only trigger on button press, not release
        {
            leftPunch();
        }
    }

    private void RightPunch()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayPoint.position, transform.up, raycastDistance, PlayerLayer);

        Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.red, raycastDistance);
        if (hit.collider != null && hit.collider.CompareTag(OtherPlayersTag))
        {
            OtherPlayersHP = hit.collider.GetComponent<BoxingHPManager>();
            OtherPlayersHP.isTakingRightPunch = true;

        }
    }

    private void leftPunch()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayPoint.position, transform.up, raycastDistance, PlayerLayer);

        Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.red, raycastDistance);

        if (hit.collider != null && hit.collider.CompareTag(OtherPlayersTag))
        {
            OtherPlayersHP = hit.collider.GetComponent<BoxingHPManager>();
            OtherPlayersHP.isTakingLeftPunch = true;
            


        }

    }



    public void OnLeftblock(InputAction.CallbackContext context)
    {
        if (context.performed) // Only trigger on button press, not release
        {
            isBlockingLeft = true;
        }
        else if (context.canceled)
        {
            isBlockingLeft = false;
        }
    }

    public void OnRightblock(InputAction.CallbackContext context)
    {
        if (context.performed) // Only trigger on button press, not release
        {
            isBlockingRight = true;
        }
        else if (context.canceled)
        {
            isBlockingRight = false;
        }
    }



    void FixedUpdate()
    {
        HandleMovement();
        if (playerInputManager.playerCount == 2)
        {
            if (playerInput.playerIndex == 0)
            {
                if (OtherPlayer == null)
                {
                    OtherPlayer = GameObject.FindGameObjectWithTag(OtherPlayersTag);
                }
            }
            else if (playerInput.playerIndex == 1)
            {
                if (OtherPlayer == null)
                {
                    OtherPlayer = GameObject.FindGameObjectWithTag(OtherPlayersTag);
                }
            }
        }

        if (OtherPlayer != null)
        {
           Vector3 direction = OtherPlayer.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -angle) ;
        }

        if (playerHPManager.HP <= 0)
        {
            if (playerInput.playerIndex == 0)
            {
                RoundManager.Player2WinIndex++;
                playerHPManager.HP = 100;
            }
            else if (playerInput.playerIndex ==1)
            {
                RoundManager.Player1WinIndex++;
                playerHPManager.HP = 100;

            }
        }
    }

    private void HandleMovement()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    // Optional: Visual feedback for grab range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * raycastDistance);
    }

   
}