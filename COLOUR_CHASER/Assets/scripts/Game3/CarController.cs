using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    [Header("Car Settings")]
    public float acceleration = 8f;
    public float reverse = 8f;
    public float steering = 200f;
    public float drag = 3f;
    public GameObject Position1;
    public GameObject Position2;

    private float moveInput;
    [SerializeField]
    private float turnInput;
    [SerializeField]
    private TrailRenderer[] trail;
    private SpriteRenderer spriteRnd;


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
            spriteRend.color = Color.blue;
            gameObject.tag = "Player1";
           
        }
        else if (playerInput.playerIndex == 1)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            spriteRend.color = Color.red;
            gameObject.tag = "Player2";
        }

        spriteRnd = GetComponent<SpriteRenderer>();
        FindObjectOfType<CarGameManager>().RegisterPlayer();
    }

    public void OnDrive(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    public void OnReverse(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }

    void FixedUpdate()
    {
        Vector2 forward = transform.up * (moveInput * acceleration);
        rb.AddForce(forward);

        Vector2 backward = transform.up * (moveInput * reverse);
        rb.AddForce(backward);

        float speedFactor = rb.velocity.magnitude / 5f;
        float rotationAmount = -turnInput * steering * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);


        rb.velocity = rb.velocity * (1 - drag * Time.fixedDeltaTime);
    }

    private void Update()
    {
        foreach(var t in trail)
        {
            t.startColor = spriteRnd.color;
            t.endColor = spriteRnd.color;
        }
    }
}
