using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
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
    [SerializeField]
    private Sprite player1, player2;
    private AudioSource audioSource;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        if (playerInput.playerIndex == 0)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            spriteRend.sprite = player1;
            gameObject.tag = "Player1";
        }
        else if (playerInput.playerIndex == 1)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            spriteRend.sprite = player2;
            gameObject.tag = "Player2";

        }

        Boxc = GetComponent<BoxCollider2D>();

        OGposition = transform.position;
        audioSource = GetComponent<AudioSource>();
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
    }

    void FixedUpdate()
    {
        // Movement
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }

    void CheckGrounded()
    {
        // Single raycast down
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);
        isGrounded = hit.collider != null;

        // Visual debug
        if (isGrounded)
        {
            Debug.DrawRay(transform.position, Vector2.down * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        audioSource.Play();

        Debug.Log("Jump!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Baloon"))
        {
            if (playerInput.playerIndex == 0)
            {
                SpriteRenderer baloonSprite = collision.gameObject.GetComponent<SpriteRenderer>();
                baloonSprite.color = Color.red;
            }
            else if (playerInput.playerIndex == 1)
            {
                SpriteRenderer baloonSprite = collision.gameObject.GetComponent<SpriteRenderer>();
                baloonSprite.color = Color.blue;
            }
        }
        else if (collision.collider.CompareTag("Kill box"))
        {
            transform.position = OGposition;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Boxc.isTrigger = false;
        }
        else if (collision.CompareTag("Floor"))
        {
            Boxc.isTrigger = false;
        }
        else if (collision.CompareTag("Baloon"))
        {
            Boxc.isTrigger = false;
            if (playerInput.playerIndex == 0)
            {
                SpriteRenderer baloonSprite = collision.gameObject.GetComponent<SpriteRenderer>();
                baloonSprite.color = Color.red;
            }
            else if (playerInput.playerIndex == 1)
            {
                SpriteRenderer baloonSprite = collision.gameObject.GetComponent<SpriteRenderer>();
                baloonSprite.color = Color.blue;
            }
        }
        else if (collision.CompareTag("Kill box"))
        {
            transform.position = OGposition;
        }
    }


    IEnumerator Dash()
    {
        isDashing = true;
        Boxc.isTrigger = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
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
        if (!isDashing)
        {
            StartCoroutine(Dash());
        }
    }
}