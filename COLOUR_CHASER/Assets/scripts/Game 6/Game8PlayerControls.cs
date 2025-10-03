using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Game8playercontrols : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public float speed = 5f;
    private PlayerInput playerInput;
    public AudioSource audioSource;
    private Animator animator;
    [SerializeField]
    private List<bool> Guns;
    private bool hasSniper, hasShotgun, hasSmg, hasAR, hasPistol, hasLazer, hasBazooka;
    public bool isShooting;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (playerInput.playerIndex == 0)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        }
        else if (playerInput.playerIndex == 1)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        }

    }

    private void Update()
    {
        hasSniper = Guns[0];
        hasShotgun = Guns[1];
        hasSmg = Guns[2];
        hasAR = Guns[3];
        hasPistol = Guns[4];
        hasLazer = Guns[5];
        hasBazooka = Guns[6];
    }

    // Called by PlayerInput when "Move" action is triggered
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnGameSelection(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("GameSelect");
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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isShooting = true;

            if (hasSniper)
            {

            }
            else if (hasPistol)
            {

            }
            else if (hasLazer)
            {

            }
            else if (hasBazooka)
            {

            }
            else if (hasShotgun)
            {

            }
            else if (hasAR)
            {

            }
            else if (hasSmg)
            {

            }
        }
        else if (context.canceled)
        {
            isShooting = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Sniper"))
        {

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

}