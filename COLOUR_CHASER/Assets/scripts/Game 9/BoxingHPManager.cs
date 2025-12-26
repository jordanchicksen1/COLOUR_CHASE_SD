using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BoxingHPManager : MonoBehaviour
{
    private BoxingController BoxingScrpit;

    [SerializeField] private int MaxDamage, BlockedDamage;
    [SerializeField] public int HP;

    public bool isTakingLeftPunch, isTakingRightPunch;

    private Rigidbody2D rb;
    [SerializeField] private float knockbackForce = 8f;

    private GameObject CountDownHolder;
    public TextMeshProUGUI CountDownText;

    [Header("UI")]
    public Image healthFillImage;

    public bool GameStarted;

    private PlayerInput playerInput;
    private void Start()
    {
        BoxingScrpit = GetComponent<BoxingController>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        CountDownHolder = GameObject.FindGameObjectWithTag("Timer");
        CountDownText = CountDownHolder.GetComponent<TextMeshProUGUI>();

        HP = 100;
        UpdateHealthBar();

        BoxingHPManager hp = GetComponent<BoxingHPManager>();

        if (playerInput.playerIndex == 0)
        {
            hp.healthFillImage = GameObject.Find("P1_HealthFill").GetComponent<Image>();
        }
        else if (playerInput.playerIndex == 1)
        {
            hp.healthFillImage = GameObject.Find("P2_HealthFill").GetComponent<Image>();
        }
    }

    private void Update()
    {
        if (isTakingLeftPunch)
        {
            TakeDamageleft();
        }
        else if (isTakingRightPunch)
        {
            TakeDamageRight();
        }
    }

    /* ----------------- HEALTH BAR ----------------- */

    void UpdateHealthBar()
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = (float)HP / 100f;
        }
    }

    /* ----------------- DAMAGE ----------------- */

    void TakeDamageRight()
    {
        if (!BoxingScrpit.isBlockingRight)
            StartCoroutine(TakeDamage());
        else
            StartCoroutine(TakeBlockDamage());
    }

    void TakeDamageleft()
    {
        if (!BoxingScrpit.isBlockingLeft)
            StartCoroutine(TakeDamage());
        else
            StartCoroutine(TakeBlockDamage());
    }

    IEnumerator TakeDamage()
    {
        isTakingRightPunch = false;
        isTakingLeftPunch = false;

        Debug.Log("Punched");

        HP -= MaxDamage;
        HP = Mathf.Clamp(HP, 0, 100);

        UpdateHealthBar();
        ApplyKnockback();

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator TakeBlockDamage()
    {
        isTakingRightPunch = false;
        isTakingLeftPunch = false;

        Debug.Log("Blocked");

        HP -= BlockedDamage;
        HP = Mathf.Clamp(HP, 0, 100);

        UpdateHealthBar();

        yield return null;
    }

    /* ----------------- KNOCKBACK ----------------- */

    public void ApplyKnockback()
    {
        StartCoroutine(KnockbackRoutine());
    }

    IEnumerator KnockbackRoutine()
    {
        if (HP <= 0)
        {
            ResetPositios();
            StartCoroutine(CountDown());
            
        }
        else
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + (-transform.up * knockbackForce);

            float duration = 0.1f;
            float elapsed = 0f;

            BoxingScrpit.animationManagerScript.Knock();

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
        }
    }

    private void ResetPositios()
    {
        transform.position = BoxingScrpit.StartPosition.transform.position;

        BoxingController other = BoxingScrpit.OtherPlayer.GetComponent<BoxingController>();
        BoxingScrpit.OtherPlayer.transform.position = other.StartPosition.transform.position;
        
    }

    /* ----------------- ROUND COUNTDOWN ----------------- */

    public IEnumerator CountDown()
    {
        GameStarted = true;

        Rigidbody2D rb1 = GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = BoxingScrpit.OtherPlayer.GetComponent<Rigidbody2D>();

        rb1.bodyType = RigidbodyType2D.Static;
        rb2.bodyType = RigidbodyType2D.Static;
        
        
        
        CountDownText.text = "3";
        yield return new WaitForSeconds(1);

        CountDownText.text = "2";
        yield return new WaitForSeconds(1);

        CountDownText.text = "1";
        yield return new WaitForSeconds(1);

        UpdateHealthBar();
        CountDownText.text = "Fight";
        yield return new WaitForSeconds(1);
        

        CountDownText.text = "";

        rb1.bodyType = RigidbodyType2D.Dynamic;
        rb2.bodyType = RigidbodyType2D.Dynamic;
    }
}
