    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;

    public class TankController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private PlayerInput playerInput;

        [Header("Tank Settings")]
        public float acceleration = 8f;
        public float reverse = -6f;
        public float steering = 200f;
        public float drag = 3f;


        private float forwardInput;
        private Vector2 moveInput;  
        private Vector2 lookInput;   

    [SerializeField] private float turnInput;

        private SpriteRenderer spriteRnd;

        private int trueIndex;
        private static int assignedIndex = 0;

        [SerializeField]
        private Sprite Player1, Player2;

        [Header("Spawn Positions (Editable)")]
        [SerializeField] private GameObject player1Spawn;
        [SerializeField] private GameObject player2Spawn;

        [Header("Shooting")]
        [SerializeField] private GameObject normalBulletPrefab;
        [SerializeField] private GameObject lobBulletPrefab;
        [SerializeField] private Transform firePoint;

        [SerializeField] private float shotForce = 20f;
        [SerializeField] private float lobForce = 10f;

        [Header("Shot Cooldowns")]
        [SerializeField] private float normalShotCooldown = 0.3f;
        [SerializeField] private float lobShotCooldown = 1f;

        private float nextNormalShotTime = 0f;
        private float nextLobShotTime = 0f;
        public int PlayerIndex => trueIndex;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        trueIndex = playerInput.playerIndex;
    }


    private void Start()
        {
            if (trueIndex == 0)
            {
                GetComponent<SpriteRenderer>().sprite = Player1;
                gameObject.tag = "Player1";
                player1Spawn = GameObject.FindGameObjectWithTag("p1");
                transform.position = player1Spawn.transform.position;
            }
            else if (trueIndex == 1)
            {
                GetComponent<SpriteRenderer>().sprite = Player2;
                gameObject.tag = "Player2";
                player2Spawn = GameObject.FindGameObjectWithTag("p2");
                transform.position = player2Spawn.transform.position;
            }

            spriteRnd = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnFireNormal(InputAction.CallbackContext context)
    {
        if (context.started && Time.time >= nextNormalShotTime)
        {
            ShootNormal();
            nextNormalShotTime = Time.time + normalShotCooldown;
        }
    }

    public void OnFireLob(InputAction.CallbackContext context)
    {
        if (context.started && Time.time >= nextLobShotTime)
        {
            ShootLob();
            nextLobShotTime = Time.time + lobShotCooldown;
        }
    }
    private void ShootNormal()
    {
        GameObject b = Instantiate(normalBulletPrefab, firePoint.position, firePoint.rotation);
        b.GetComponent<Bullet>().SetOwner(trueIndex);

        Rigidbody2D rb2 = b.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
    }

    private void ShootLob()
    {
        GameObject b = Instantiate(lobBulletPrefab, firePoint.position, firePoint.rotation);
        b.GetComponent<Bullet>().SetOwner(trueIndex);

        Rigidbody2D rb2 = b.GetComponent<Rigidbody2D>();
        Vector2 dir = (firePoint.up + firePoint.right * 0.4f).normalized;
        rb2.AddForce(dir * lobForce, ForceMode2D.Impulse);
    }



    void FixedUpdate()
        {

        // Forward / reverse based on left stick Y
        float moveValue = moveInput.y * acceleration;
        rb.AddForce(transform.up * moveValue);

        // Apply drag
        rb.velocity = rb.velocity * (1 - drag * Time.fixedDeltaTime);
    }

        private void Update()
        {
          if (lookInput.sqrMagnitude > 0.1f)
          {
              float angle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg - 90f;
              rb.MoveRotation(angle);
          }
        }

        public void OnGameSelection(InputAction.CallbackContext context)
        {
            SceneManager.LoadScene("GameSelect");
        }

        public void ResetToSpawn()
        {
            if (trueIndex == 0 && player1Spawn != null)
            {
                transform.position = player1Spawn.transform.position;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.rotation = 0f;
            }
            else if (trueIndex == 1 && player2Spawn != null)
            {
                transform.position = player2Spawn.transform.position;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.rotation = 0f;
            }
        }

    }
