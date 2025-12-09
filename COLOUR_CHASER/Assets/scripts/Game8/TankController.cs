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
        private float reverseInput;
        [SerializeField] private float turnInput;

        private SpriteRenderer spriteRnd;

        [Header("Camera Settings")]
        public GameObject cameraPrefab; 
        private Camera playerCam;

        private int trueIndex;
        private static int assignedIndex = 0;

        [SerializeField]
        private Sprite Player1, Player2;

        [Header("Spawn Positions (Editable)")]
        [SerializeField] private GameObject player1Spawn;
        [SerializeField] private GameObject player2Spawn;


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

            GameObject camObj = Instantiate(cameraPrefab);
            playerCam = camObj.GetComponent<Camera>();

            playerCam.transform.position = transform.position + new Vector3(0, 0, -10);

        if (playerInput.playerIndex == 0)
            playerCam.rect = new Rect(0, 0, 0.5f, 1); 
        else if (playerInput.playerIndex == 1)
            playerCam.rect = new Rect(0.5f, 0, 0.5f, 1); 
    }

        public void OnDrive(InputAction.CallbackContext context) => forwardInput = context.ReadValue<float>();
        public void OnReverse(InputAction.CallbackContext context) => reverseInput = context.ReadValue<float>();
        public void OnRotate(InputAction.CallbackContext context) => turnInput = context.ReadValue<float>();


        void FixedUpdate()
        {
            float moveValue = (forwardInput * acceleration) - (reverseInput * reverse);
            rb.AddForce(transform.up * moveValue);

            float rotationAmount = -turnInput * steering * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation + rotationAmount);

            rb.velocity = rb.velocity * (1 - drag * Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (playerCam != null)
            playerCam.transform.position = transform.position + new Vector3(0, 0, -10);

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
