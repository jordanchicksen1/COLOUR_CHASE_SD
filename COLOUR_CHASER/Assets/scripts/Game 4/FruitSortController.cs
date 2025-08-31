using System;
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
    [SerializeField]
    private int raycastDistance;
    private float turnInput;
    [SerializeField]
    private int rotateSpeed = 20;
    public LayerMask FruitLayer;
    public Transform Holder;
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

    public void OnGrab(InputAction.CallbackContext context)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, raycastDistance, FruitLayer);
        Debug.DrawRay(transform.position, transform.up, Color.red, raycastDistance);
        if (hit.collider.CompareTag("Apple"))
        {
            if (hit.collider.gameObject.transform.parent != Holder.transform)
            {
                hit.collider.gameObject.transform.parent = Holder.transform;
                hit.collider.gameObject.transform.position = Holder.position;
            }
            else if (hit.collider.gameObject.transform.parent == Holder.transform)
            {
                hit.collider.gameObject.transform.parent = null;
            }
        }

    }


        public void OnRotate(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);

        float speedFactor = rb.velocity.magnitude / 5f;
        float rotationAmount = -turnInput * rotateSpeed *Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);

    }

}
