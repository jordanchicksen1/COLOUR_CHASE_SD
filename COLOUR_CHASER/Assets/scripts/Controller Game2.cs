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

    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("Jump!");
    }
}