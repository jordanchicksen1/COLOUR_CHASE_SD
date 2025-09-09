using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class FruitSortController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    [SerializeField] private int raycastDistance = 2;

    [Header("Rotation Settings")]
    [SerializeField] private int rotateSpeed = 20;

    [Header("References")]
    public LayerMask fruitLayer;
    public Transform holder;

    private Vector2 moveInput;
    private float turnInput;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private GameObject currentlyHeldFruit;
    private GameObject p1Position;
    public Sprite[] sprites;

    private void Start()
    {
        if (playerInput.playerIndex == 0)
        {
            p1Position = GameObject.FindGameObjectWithTag("p1");
            transform.position = p1Position.transform.position;
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = sprites[0];
        }
        else if (playerInput.playerIndex == 1)
        {
            p1Position = GameObject.FindGameObjectWithTag("p2");
            transform.position = p1Position.transform.position;
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = sprites[1];
        }
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

    // Rotation input
    public void OnRotate(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }

    // Grab/Release input
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed) // Only trigger on button press, not release
        {
            if (currentlyHeldFruit == null)
            {
                TryGrabFruit();
            }
            else
            {
                ReleaseFruit();
            }
        }
    }

    private void TryGrabFruit()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            transform.up,
            raycastDistance,
            fruitLayer
        );

        Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.red, 0.5f);

        if (hit.collider != null && hit.collider.CompareTag("Apple"))
        {
            GrabFruit(hit.collider.gameObject);
        }
    }

    private void GrabFruit(GameObject fruit)
    {
        currentlyHeldFruit = fruit;

        // Disable physics while held
        Rigidbody2D fruitRb = fruit.GetComponent<Rigidbody2D>();
        if (fruitRb != null)
        {
            fruitRb.simulated = false;
        }

        // Parent to holder and position
        fruit.transform.SetParent(holder);
        fruit.transform.localPosition = Vector3.zero;
        fruit.transform.localRotation = Quaternion.identity;
        FruitManager fruitSCript = currentlyHeldFruit.GetComponent<FruitManager>();
        fruitSCript.OGPosotion = fruit.transform.position;
    }

    private void ReleaseFruit()
    {
        if (currentlyHeldFruit == null) return;

        // Re-enable physics
        Rigidbody2D fruitRb = currentlyHeldFruit.GetComponent<Rigidbody2D>();
        if (fruitRb != null)
        {
            fruitRb.simulated = true;
        }

        // Unparent and clear reference
        currentlyHeldFruit.transform.SetParent(null);
        currentlyHeldFruit = null;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        // Smooth rotation based on input
        float rotationAmount = -turnInput * rotateSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);
    }

    // Optional: Visual feedback for grab range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * raycastDistance);
    }
}