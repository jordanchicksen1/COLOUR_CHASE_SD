using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FruitSortController : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public float speed = 5f;
    private PlayerInput playerInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
    }

    // Called by PlayerInput when "Move" action is triggered
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //  rb.AddForce(Vector2.up * 50f, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, 5);
            Debug.Log("Jump");

        }
    }



    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

}
