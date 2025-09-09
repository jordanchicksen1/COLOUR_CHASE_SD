using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformCharacterController : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Detection")]
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask groundLayer;

    private PlayerInput playerInput;
    private bool isGrounded;
    private BoxCollider2D Boxc;
    private bool isDashing;
    private Vector2 OGposition;

    [Header("Sprite Children")]
    [SerializeField] private GameObject player1SpriteChild;
    [SerializeField] private GameObject player2SpriteChild;

    private SpriteRenderer activeSpriteRend;

    [Header("Spawn Positions (Editable)")]
    [SerializeField] private Vector2 player1Spawn = new Vector2(-5f, 0f);
    [SerializeField] private Vector2 player2Spawn = new Vector2(5f, 0f);

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float jumpVolume = 1f;
    private AudioSource audioSource;   

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        player1SpriteChild.SetActive(false);
        player2SpriteChild.SetActive(false);

        if (playerInput.playerIndex == 0)
        {
            player1SpriteChild.SetActive(true);
            gameObject.tag = "Player1";
            activeSpriteRend = player1SpriteChild.GetComponent<SpriteRenderer>();

            transform.position = player1Spawn;
            OGposition = player1Spawn;
        }
        else if (playerInput.playerIndex == 1)
        {
            player2SpriteChild.SetActive(true);
            gameObject.tag = "Player2";
            activeSpriteRend = player2SpriteChild.GetComponent<SpriteRenderer>();

            transform.position = player2Spawn;
            OGposition = player2Spawn;
        }

        Boxc = GetComponent<BoxCollider2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Jump();
        }
    }

    void Update()
    {
        CheckGrounded();

        if (moveInput.x > 0.1f)
        {
            activeSpriteRend.flipX = false; 
        }
        else if (moveInput.x < -0.1f)
        {
            activeSpriteRend.flipX = true;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }

    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (isGrounded)
            Debug.DrawRay(transform.position, Vector2.down * hit.distance, Color.green);
        else
            Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound, jumpVolume);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Kill box"))
        {
            transform.position = OGposition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kill box"))
        {
            transform.position = OGposition;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        Boxc.isTrigger = true;
        rb.gravityScale = 0;
        speed += 5;
        yield return new WaitForSeconds(0.5f);
        Boxc.isTrigger = false;
        rb.gravityScale = 2;
        speed -= 5;
        isDashing = false;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }
}
