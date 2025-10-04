using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BrawlerController : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Detection")]
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask groundLayer;

    private PlayerInput playerInput;
    private bool isGrounded;
    private BoxCollider2D Boxc;
    private bool isDashing;
    private Vector2 OGposition;

    [Header("Sprite Children")]


    [Header("Spawn Positions (Editable)")]
    [SerializeField] private GameObject player1Spawn;
    [SerializeField] private GameObject player2Spawn;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float jumpVolume = 1f;
    private AudioSource audioSource;
    private Animator animator;

    [Header("Pull Settings")]
    [SerializeField] private float pullRange = 1.5f;
    [SerializeField] private float pullSpeed = 3f;
    [SerializeField] private LayerMask pullableLayer;
    private GameObject objectBeingPulled;
    private bool isPulling;
    private FixedJoint2D pullJoint;

    [SerializeField] private Transform weaponHoldPoint; 
    private Weapon currentWeapon;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (playerInput.playerIndex == 0)
        {
            gameObject.tag = "Player1";
            player1Spawn = GameObject.FindGameObjectWithTag("p1");
            transform.position = player1Spawn.transform.position;
        }
        else if (playerInput.playerIndex == 1)
        {
            gameObject.tag = "Player2";
            player2Spawn = GameObject.FindGameObjectWithTag("p2");
            transform.position = player2Spawn.transform.position;
        }

        Boxc = GetComponent<BoxCollider2D>();
    }

    public void OnGameSelection(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("GameSelect");
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

    public void OnAbility(InputAction.CallbackContext context)
    {
    }

    void Update()
    {
        CheckGrounded();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

        if (playerInput.playerIndex == 0)
        {
            animator.SetBool("P2", true);
            if (moveInput.x > 0)
            {
                animator.SetBool("Walk2", true);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (moveInput.x < 0)
            {
                animator.SetBool("Walk2", true);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                animator.SetBool("Walk2", false);
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (playerInput.playerIndex == 1)
        {
            if (moveInput.x > 0)
            {
                animator.SetBool("Walk", true);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (moveInput.x < 0)
            {
                animator.SetBool("Walk", true);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                animator.SetBool("Walk", false);
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    void CheckGrounded()
    {
        int combinedLayers = groundLayer | pullableLayer;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, combinedLayers);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Debug.DrawRay(transform.position, Vector2.down * (hit.collider ? hit.distance : raycastDistance),
                      isGrounded ? Color.green : Color.red);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound, jumpVolume);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Kill box"))
        {
            transform.position = OGposition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kill box"))
        {
            transform.position = OGposition;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        Boxc.isTrigger = true;
        rb.gravityScale = 0;
        speed += 5;
        yield return new WaitForSeconds(0.5f);
        Boxc.isTrigger = false;
        rb.gravityScale = 2;
        speed -= 5;
        isDashing = false;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (var hit in hits)
            {
                Weapon weapon = hit.GetComponent<Weapon>();
                if (weapon != null)
                {
                    EquipWeapon(weapon);
                    break;
                }
            }
        }
    }

    void EquipWeapon(Weapon weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = weapon;

        weapon.transform.SetParent(weaponHoldPoint);

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        weapon.transform.localScale = Vector3.one;

        Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.simulated = false;
        }

        Collider2D col = weapon.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        Debug.Log("Equipped " + weapon.weaponType);
    }

    public void OnShoot(InputAction.CallbackContext context)//shoot
    {
        if (context.performed && currentWeapon != null)
        {
            currentWeapon.Shoot();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pullRange);
    }
}
