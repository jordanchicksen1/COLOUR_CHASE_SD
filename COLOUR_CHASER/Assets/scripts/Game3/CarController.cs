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
    public float steering = 200f;     
    public float drag = 3f;          

    private float moveInput;         
    private float turnInput;          

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveInput = input.y;  
        turnInput = input.x;   
    }

    void FixedUpdate()
    {
        Vector2 forward = transform.up * (moveInput * acceleration);
        rb.AddForce(forward);

        float speedFactor = rb.velocity.magnitude / 5f; 
        float rotationAmount = -turnInput * steering * speedFactor * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);

        rb.velocity = rb.velocity * (1 - drag * Time.fixedDeltaTime);
    }

    
    public void OnJump(InputAction.CallbackContext context)
    {
        
    }
}
