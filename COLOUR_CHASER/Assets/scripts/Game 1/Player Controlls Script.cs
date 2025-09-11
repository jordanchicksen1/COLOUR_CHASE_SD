using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public float speed = 5f;
    private PlayerBlockChecker playerBlockCheckerScript;
    private GameObject BlockChecker;
    private PlayerInput playerInput;
    public Sprite[] sprites;
    public AudioSource audioSource;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        BlockChecker = GameObject.FindGameObjectWithTag("BlockChecker");
        playerBlockCheckerScript = BlockChecker.GetComponent<PlayerBlockChecker>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (playerInput.playerIndex == 0)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            spriteRend.sprite = sprites[playerInput.playerIndex];
        }
        else if (playerInput.playerIndex == 1)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            spriteRend.sprite = sprites[playerInput.playerIndex];
        }

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
        if (playerInput.playerIndex == 0)
        {
            animator.SetBool("P2", true);
            if (moveInput.x > 0)
            {
                animator.SetBool("Walk2", true);
                SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
                spriteRend.flipX = true;

            }
            else if (moveInput.x < 0)
            {
                animator.SetBool("Walk2", true);
                SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
                spriteRend.flipX = false;
            }
            else if (moveInput.x == 0)
            {
                animator.SetBool("Walk2", false);
                SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
                spriteRend.flipX = true;
            }
        }
        else if (playerInput.playerIndex == 1)
        {
            if (moveInput.x > 0)
            {
                animator.SetBool("Walk", true);
                SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
                spriteRend.flipX = false;

            }
            else if (moveInput.x < 0)
            {
                animator.SetBool("Walk", true);
                SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
                spriteRend.flipX = true;
            }
            else if (moveInput.x == 0)
            {
                animator.SetBool("Walk", false);
                SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
                spriteRend.flipX = false;
            }
        }
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