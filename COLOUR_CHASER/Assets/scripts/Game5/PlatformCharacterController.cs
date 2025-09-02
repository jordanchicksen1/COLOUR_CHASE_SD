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

    // 🔹 Children with different sprites
    [Header("Sprite Children")]
    [SerializeField] private GameObject player1SpriteChild;
    [SerializeField] private GameObject player2SpriteChild;

    private SpriteRenderer activeSpriteRend;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        // Ensure both are disabled first
        player1SpriteChild.SetActive(false);
        player2SpriteChild.SetActive(false);

        // Enable correct one based on player index
        if (playerInput.playerIndex == 0)
        {
            player1SpriteChild.SetActive(true);
            gameObject.tag = "Player1";
            activeSpriteRend = player1SpriteChild.GetComponent<SpriteRenderer>();
        }
        else if (playerInput.playerIndex == 1)
        {
            player2SpriteChild.SetActive(true);
            gameObject.tag = "Player2";
            activeSpriteRend = player2SpriteChild.GetComponent<SpriteRenderer>();
        }

        Boxc = GetComponent<BoxCollider2D>();
        OGposition = transform.position;
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

        // 🔹 Flip the active sprite only
        if (moveInput.x > 0.1f)
        {
            activeSpriteRend.flipX = false; // facing right
        }
        else if (moveInput.x < -0.1f)
        {
            activeSpriteRend.flipX = true; // facing left
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
