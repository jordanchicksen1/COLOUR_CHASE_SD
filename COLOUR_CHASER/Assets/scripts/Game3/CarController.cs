using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    [Header("Car Settings")]
    public float acceleration = 8f;
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

    [Header("Abilities")]
    public GameObject tirePrefab;
    public GameObject oilPrefab;
    public GameObject shockwavePrefab;
    private string currentAbility = "";
    private bool abilityReady = false;

    [Header("Ability Visuals")]
    public Transform abilityHolder;  
    private GameObject abilityIconInstance;
    [SerializeField]
    private Sprite Player1, Player2;


    [Header("Ability Audio")]
    [SerializeField] private AudioClip tireSound;
    [SerializeField] private AudioClip oilSound;
    [SerializeField] private AudioClip shockwaveSound;
    [SerializeField] private float abilityVolume = 1f;
    [SerializeField] private AudioClip speedSound;
    private bool isDrivingSoundPlaying = false;

    [Header("Spawn Positions (Editable)")]
    [SerializeField] private GameObject player1Spawn;
    [SerializeField] private GameObject player2Spawn;


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

        spriteRnd = GetComponent<SpriteRenderer>();
        FindObjectOfType<CarGameManager>().RegisterPlayer();
    }

    public void OnDrive(InputAction.CallbackContext context) => forwardInput = context.ReadValue<float>();
    public void OnReverse(InputAction.CallbackContext context) => reverseInput = context.ReadValue<float>();
    public void OnRotate(InputAction.CallbackContext context) => turnInput = context.ReadValue<float>();

    public void OnAbility(InputAction.CallbackContext context)
    {
        if (context.performed && abilityReady) UseAbility();
    }

    void FixedUpdate()
    {
        float moveValue = (forwardInput * acceleration) - (reverseInput * reverse);
        rb.AddForce(transform.up * moveValue);

        float rotationAmount = -turnInput * steering * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);

        rb.velocity = rb.velocity * (1 - drag * Time.fixedDeltaTime);

        if (Mathf.Abs(moveValue) > 0.1f) 
        {
           
        }
        else
        {
           
        }
    }

    private void Update()
    {
        foreach (var t in trail)
        {
           if (playerInput.playerIndex == 0)
            {
                t.startColor = Color.blue;
                t.endColor = Color.blue;
            }else if (playerInput.playerIndex == 1)
            {
                t.startColor = Color.red;
                t.endColor = Color.red;
            }
        }
    }

    public void GiveAbility(string abilityName)
    {
        if (!abilityReady)
        {
            currentAbility = abilityName;
            abilityReady = true;
            Debug.Log(gameObject.name + " got ability: " + abilityName);

            if (abilityIconInstance != null)
                Destroy(abilityIconInstance);

            GameObject iconPrefab = Resources.Load<GameObject>("AbilityIcons/" + abilityName + "Icon");
            if (iconPrefab != null && abilityHolder != null)
            {
                abilityIconInstance = Instantiate(iconPrefab, abilityHolder.position, Quaternion.identity, abilityHolder);
            }
        }
    }

    private void UseAbility()
    {
        GameObject prefab = null;

        switch (currentAbility)
        {
            case "Tire":
                prefab = Resources.Load<GameObject>("Abilities/TirePrefab");
                if (prefab != null)
                    Instantiate(prefab, Position1.transform.position, transform.rotation);

                if (tireSound != null)
                    AudioSource.PlayClipAtPoint(tireSound, transform.position, abilityVolume);
                break;

            case "Oil":
                prefab = Resources.Load<GameObject>("Abilities/OilPrefab");
                if (prefab != null)
                    Instantiate(prefab, Position2.transform.position, Quaternion.identity);

                if (oilSound != null)
                    AudioSource.PlayClipAtPoint(oilSound, transform.position, abilityVolume);
                break;


            case "Speed":
                StartCoroutine(SpeedBoost());

                if (speedSound != null)
                    AudioSource.PlayClipAtPoint(speedSound, transform.position, abilityVolume);
                break;

            case "Shockwave":
                prefab = Resources.Load<GameObject>("Abilities/ShockwavePrefab");
                if (prefab != null)
                    Instantiate(prefab, transform.position, Quaternion.identity);

                if (shockwaveSound != null)
                    AudioSource.PlayClipAtPoint(shockwaveSound, transform.position, abilityVolume);
                break;
        }

        if (abilityIconInstance != null)
        {
            Destroy(abilityIconInstance);
            abilityIconInstance = null;
        }

        currentAbility = "";
        abilityReady = false;
    }

    public void OnGameSelection(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("GameSelect");
    }

    public void ResetToSpawn()
    {
        if (playerInput.playerIndex == 0 && player1Spawn != null)
        {
            transform.position = player1Spawn.transform.position;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.rotation = 0f;
        }
        else if (playerInput.playerIndex == 1 && player2Spawn != null)
        {
            transform.position = player2Spawn.transform.position;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.rotation = 0f;
        }
    }
    private IEnumerator SpeedBoost()
    {
        float originalAccel = acceleration;
        acceleration *= 2f;
        yield return new WaitForSeconds(3f);
        acceleration /= 2f;
    }
}
