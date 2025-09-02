using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformCharacterController : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public float speed = 5f;
    private PlayerBlockChecker playerBlockCheckerScript;
    private GameObject BlockChecker;
    private PlayerInput playerInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        BlockChecker = GameObject.FindGameObjectWithTag("BlockChecker");
        playerBlockCheckerScript = BlockChecker.GetComponent<PlayerBlockChecker>();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Right"))
        {
            playerBlockCheckerScript.Correctblock[playerInput.playerIndex] = true;
            collision.gameObject.tag = "OnBlock";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("OnBlock"))
        {
            playerBlockCheckerScript.Correctblock[playerInput.playerIndex] = false;
            collision.gameObject.tag = "Right";

        }
    }
}
