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
    private bool canPickup;
    [SerializeField]
    private Transform holdingPosition;
    [SerializeField]
    private Transform GunPoint;
    [SerializeField]
    private Camera Cam;
    [SerializeField]
    private int MaxScope = -14;
    private ShootManager shootManagerScript;
    //Gun rotation
    private Vector2 lookInput;


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

        Cam.transform.localPosition = new Vector3(0, 0, MaxScope);

        if (shootManagerScript != null)
        {
            for (int i = 0; i < Guns.Count; i++)
            {
                shootManagerScript.Guns[i] = Guns[i];
            }
        }

        //Gun Rotattion
        if (lookInput.sqrMagnitude > 0.01f) // avoid noise when stick is idle
        {
            float angle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg;
            holdingPosition.rotation = Quaternion.Euler(0f, 0f, angle);
        }

    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
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

    public void OnPickup(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            canPickup = true;
        }
        else if (context.canceled)
        {
            canPickup = false;
        }
    }

    public void OnScope(InputAction.CallbackContext context)
    {
       if (context.canceled)
        {
            if (hasSniper)
            {
                if (Cam.transform.localPosition.z != -41)
                {
                    MaxScope -= 7;
                }
                else if(Cam.transform.localPosition.z == -41)
                {
                    MaxScope = -20;
                }
            }
            else if (hasPistol)
            {
                if (Cam.transform.localPosition.z != -20)
                {
                    MaxScope -= 0;
                }
               
            }
            else if (hasLazer)
            {
                if (Cam.transform.localPosition.z != -34)
                {
                    MaxScope -= 7;
                }
                else if (Cam.transform.localPosition.z == -34)
                {
                    MaxScope -= -20;
                }

            }
            else if (hasBazooka)
            {
                if (Cam.transform.localPosition.z != -34)
                {
                    MaxScope -= 7;
                }
                else if (Cam.transform.localPosition.z == -34)
                {
                    MaxScope -= -20;
                }

            }
            else if (hasShotgun)
            {
                if (Cam.transform.localPosition.z != -27)
                {
                    MaxScope -= 7;
                }
                else if (Cam.transform.localPosition.z == -27)
                {
                    MaxScope -= -20;
                }

            }
            else if (hasAR)
            {
                if (Cam.transform.localPosition.z != -34)
                {
                    MaxScope -= 7;
                }
                else if (Cam.transform.localPosition.z == -34)
                {
                    MaxScope -= -20;
                }

            }
            else if (hasSmg)
            {
                if (Cam.transform.localPosition.z != -27)
                {
                    MaxScope -= 7;
                }
                else if (Cam.transform.localPosition.z == -27)
                {
                    MaxScope -= -20;
                }

            }
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            shootManagerScript.isShooting = true;
        }
        else if (context.canceled)
        {
            shootManagerScript.isShooting = false;
            shootManagerScript.isRunning = false;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Sniper"))
        {
            if (canPickup)
            {
                collision.gameObject.transform.position = GunPoint.position;
                collision.gameObject.transform.parent = GunPoint;
                Guns[0] = true;
                shootManagerScript = collision.gameObject.GetComponent<ShootManager>();
            }
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