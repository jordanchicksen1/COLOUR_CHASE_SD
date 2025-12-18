using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RacingController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    [Header("Car Settings")]
    public float acceleration = 8f;
    [SerializeField]
    private int MaxSpeed, MinSpeed;
    public float reverse = -6f;
    public float steering = 200f;
    public float drag = 3f;

    public GameObject Position1;
    public GameObject Position2;

    private float forwardInput;
    private float reverseInput;
    [SerializeField] private float turnInput;

    [SerializeField] private TrailRenderer[] trail;
    private SpriteRenderer spriteRnd;

    [SerializeField]
    private Sprite Player1, Player2;




    [Header("Spawn Positions (Editable)")]
    [SerializeField] private GameObject player1Spawn;
    [SerializeField] private GameObject player2Spawn;

    [SerializeField]
    private bool isAccelerating, isBreaking, isOutOfBounds;

    [Header("Game Controller")]
    private GameObject GameControllerHolder;
    private LapManager lapManagerManagerScript;

    public GameObject ExplotionParticle;

    public AudioSource boostSFX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {

        if (playerInput.playerIndex == 0)
        {
            GetComponent<SpriteRenderer>().sprite = Player1;
            gameObject.tag = "Player1";

            player1Spawn = GameObject.FindGameObjectWithTag("p1");
            transform.position = player1Spawn.transform.position;
        }
        else if (playerInput.playerIndex == 1)
        {
            GetComponent<SpriteRenderer>().sprite = Player2;
            gameObject.tag = "Player2";
            player2Spawn = GameObject.FindGameObjectWithTag("p2");
            transform.position = player2Spawn.transform.position;
        }
        GameControllerHolder = GameObject.FindGameObjectWithTag("GameController");
        lapManagerManagerScript = GameControllerHolder.GetComponent<LapManager>();
        spriteRnd = GetComponent<SpriteRenderer>();
    }

    public void OnDrive(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            forwardInput = context.ReadValue<float>();
            isAccelerating = true;
        }
        else if (context.canceled)
        {
            isAccelerating = false;
            acceleration = MinSpeed;
            forwardInput = context.ReadValue<float>();
        }
    }


    public void OnReverse(InputAction.CallbackContext context) => reverseInput = context.ReadValue<float>();
    public void OnRotate(InputAction.CallbackContext context) => turnInput = context.ReadValue<float>();

    public void OnBreak(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBreaking = true;
        }
        else if (context.canceled)
        {
            isBreaking = false;
            if (acceleration < MinSpeed)
            {
                acceleration = MinSpeed;
            }
        }
    }


    void FixedUpdate()
    {
        float moveValue = (forwardInput * acceleration) + (reverseInput * reverse);
        rb.AddForce(transform.up * moveValue);

        float rotationAmount = -turnInput * steering * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);

        rb.velocity = rb.velocity * (1 - drag * Time.fixedDeltaTime);

        if (isAccelerating && acceleration < MaxSpeed)
        {
            if (!isBreaking && !isOutOfBounds)
            {
                acceleration += Time.deltaTime * 3;
            }
            else if (isBreaking)
            {
                if (acceleration > 0)
                {
                    acceleration -= Time.deltaTime * 15;
                }
            }
            else if(isOutOfBounds)
            {
                if (acceleration > 10)
                {
                    acceleration -= Time.deltaTime * 10;
                }
            }
        }
        else if (!isAccelerating && acceleration > MinSpeed)
        {
            acceleration -= Time.deltaTime * 4;

        }


    }

    private void Update()
    {

    }



    public void OnGameSelection(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("GameSelect");
    }


    private IEnumerator SpeedBoost()
    {
        boostSFX.Play();
        float originalAccel = acceleration;
        acceleration *= 2f;
        foreach (var t in trail)
        {
            if (playerInput.playerIndex == 0)
            {
                t.startColor = Color.blue;
                t.endColor = Color.white;
            }
            else if (playerInput.playerIndex == 1)
            {
                t.startColor = Color.red;
                t.endColor = Color.white;
            }
        }
        yield return new WaitForSeconds(1f);
        acceleration /= 2f;
        foreach (var t in trail)
        {
            if (playerInput.playerIndex == 0)
            {
                t.startColor = Color.black;
                t.endColor = Color.black;
            }
            else if (playerInput.playerIndex == 1)
            {
                t.startColor = Color.black;
                t.endColor = Color.black;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(playerInput.playerIndex == 0)
        {
            if (collision.CompareTag("CheckPoint"))
            {
                lapManagerManagerScript.ActivateNextCheckPoint1();
            }
        }
        else if (playerInput.playerIndex == 1)
        {
            if (collision.CompareTag("CheckPoint2"))
            {
                lapManagerManagerScript.ActivateNextCheckPoint2();
            }
        }

        if (collision.CompareTag("Spikes"))
        {
            acceleration = MinSpeed;
            GameObject Explotion = Instantiate(ExplotionParticle, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(Explotion, 1);
        }
        else if (collision.CompareTag("boost"))
        {
            StartCoroutine(SpeedBoost());

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Kill box"))
        {
            isOutOfBounds = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Kill box"))
        {
            isOutOfBounds = false;
        }
    }
}
