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
     

    [SerializeField] private float turnInput;

        private SpriteRenderer spriteRnd;

        private int trueIndex;
        private static int assignedIndex = 0;

        [SerializeField]
        private Sprite Player1, Player2;

        [Header("Spawn Positions (Editable)")]
     

        [Header("Shooting")]
        [SerializeField] private GameObject normalBulletPrefab;
        [SerializeField] private GameObject lobBulletPrefab;
        [SerializeField] private Transform firePoint;

        [SerializeField] private float shotForce = 20f;
        [SerializeField] private float lobForce = 10f;

    [Header("Death FX")]
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private float respawnDelay = 2f;

    private bool isDead = false;

    [Header("Shot Cooldowns")]
        [SerializeField] private float normalShotCooldown = 0.3f;
        [SerializeField] private float lobShotCooldown = 1f;

        private float nextNormalShotTime = 0f;
        private float nextLobShotTime = 0f;
        public int PlayerIndex => trueIndex;

    public enum PowerUpType { None, Missiles, SpeedBoost }

    private PowerUpType currentPowerUp = PowerUpType.None;

    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float missileForce = 12f;

    [SerializeField] private float speedBoostMultiplier = 1.8f;
    [SerializeField] private float speedBoostDuration = 9f;

    private bool speedBoostActive = false;


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
        }
        else if (trueIndex == 1)
        {
            GetComponent<SpriteRenderer>().sprite = Player2;
            gameObject.tag = "Player2";
        }

        transform.position = GetRandomSpawnPosition();

        spriteRnd = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnFireNormal(InputAction.CallbackContext context)
    {
        if (context.started && Time.time >= nextNormalShotTime)
        {
            ShootNormal();
            nextNormalShotTime = Time.time + normalShotCooldown;
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

        rb.velocity = Vector2.zero;
        rb.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        spriteRnd.enabled = false;
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        ResetToSpawn();

        rb.simulated = true;
        GetComponent<Collider2D>().enabled = true;
        spriteRnd.enabled = true;

        isDead = false;
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
        if (moveInput.sqrMagnitude < 0.01f)
        {
            rb.velocity = Vector2.Lerp(
                rb.velocity,
                Vector2.zero,
                drag * Time.fixedDeltaTime
            );
            return;
        }

        float targetAngle =
            Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;

        rb.MoveRotation(targetAngle);

        float speed = moveInput.magnitude * acceleration;
        rb.AddForce(transform.up * speed, ForceMode2D.Force);

        rb.velocity = Vector2.Lerp(
            rb.velocity,
            Vector2.zero,
            drag * Time.fixedDeltaTime
        );
    }

        private void Update()
        {
       
        }

        public void OnGameSelection(InputAction.CallbackContext context)
        {
            SceneManager.LoadScene("GameSelect");
        }

    public void ResetToSpawn()
    {
        transform.position = GetRandomSpawnPosition();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.rotation = 0f;
    }
    private Vector3 GetRandomSpawnPosition()
    {
        string tag = trueIndex == 0 ? "p1" : "p2";
        GameObject[] spawns = GameObject.FindGameObjectsWithTag(tag);

        if (spawns.Length == 0)
        {
            Debug.LogError($"No spawn points found with tag {tag}");
            return transform.position;
        }

        return spawns[Random.Range(0, spawns.Length)].transform.position;
    }
   

    public void OnUsePowerUp(InputAction.CallbackContext context)
    {
        if (!context.started || currentPowerUp == PowerUpType.None)
            return;

        switch (currentPowerUp)
        {
            case PowerUpType.Missiles:
                FireMissiles();
                break;

            case PowerUpType.SpeedBoost:
                StartCoroutine(SpeedBoost());
                break;
        }

        currentPowerUp = PowerUpType.None;
    }
    void FireMissiles()
    {
        for (int i = 0; i < 3; i++)
        {
            float spread = Random.Range(-15f, 15f);
            Quaternion rot = firePoint.rotation * Quaternion.Euler(0, 0, spread);

            GameObject m = Instantiate(missilePrefab, firePoint.position, rot);
            m.GetComponent<Rigidbody2D>()
             .AddForce(m.transform.up * missileForce, ForceMode2D.Impulse);
        }
    }
    IEnumerator SpeedBoost()
    {
        if (speedBoostActive)
            yield break;

        speedBoostActive = true;
        acceleration *= speedBoostMultiplier;

        yield return new WaitForSeconds(speedBoostDuration);

        acceleration /= speedBoostMultiplier;
        speedBoostActive = false;
    }
    public void GivePowerUp(PowerUpType type)
    {
        currentPowerUp = type;
    }

}
