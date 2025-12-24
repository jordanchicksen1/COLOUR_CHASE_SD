using System.Collections;
using TMPro;
using UnityEngine;

public class BoxingHPManager : MonoBehaviour
{
    private BoxingController BoxingScrpit;
    [SerializeField]
    private int MaxDamage, BlockedDamage;
    [SerializeField]
    public int HP;
    public bool isTakingLeftPunch, isTakingRightPunch;
    private Rigidbody2D rb;
    [SerializeField] private float knockbackForce = 8f;
    private GameObject CountDownHolder;
    public TextMeshProUGUI CountDownText;

    private void Start()
    {
        BoxingScrpit = GetComponent<BoxingController>();
        rb = GetComponent<Rigidbody2D>();
        CountDownHolder = GameObject.FindGameObjectWithTag("Timer");
        CountDownText = CountDownHolder.GetComponent<TextMeshProUGUI>();
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

    public void ApplyKnockback()
    {
        StartCoroutine(KnockbackRoutine());
    }
    private void ResetPositios()
    {
        transform.position = BoxingScrpit.StartPosition.transform.position;
        BoxingController otherplayerScript = BoxingScrpit.OtherPlayer.GetComponent<BoxingController>();
        BoxingScrpit.OtherPlayer.transform.position = otherplayerScript.StartPosition.transform.position;
    }
    IEnumerator KnockbackRoutine()
    {
        
        if (HP <= 0)
        {
            ResetPositios();
            StartCoroutine(CountDown());
        }
        else if (HP > 0)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + (-transform.up * knockbackForce);
            float duration = 0.1f; // Adjust for how long the knockback takes
            float elapsed = 0f;
            BoxingScrpit.animationManagerScript.Knock();
            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos; // Ensure exact position
        }
    }
    public bool GameStarted;

    public IEnumerator CountDown()
    {
        GameStarted = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = BoxingScrpit.OtherPlayer.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        rb2.bodyType = RigidbodyType2D.Static;
        CountDownText.text = "3";
        yield return new WaitForSeconds(1);
        CountDownText.text = "2";
        yield return new WaitForSeconds(1);
        CountDownText.text = "1";
        yield return new WaitForSeconds(1);
        CountDownText.text = "Fight";
        yield return new WaitForSeconds(1);
        CountDownText.text = "";
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb2.bodyType = RigidbodyType2D.Dynamic;
    }

    void TakeDamageRight()
    {
        if (BoxingScrpit != null)
        {
            if (!BoxingScrpit.isBlockingRight)
            {
                StartCoroutine(TakeDamage());
            }
            else if (BoxingScrpit.isBlockingRight)
            {
                StartCoroutine(TakeBlockDamage());
            }
        }
    }

    void TakeDamageleft()
    {
        if (BoxingScrpit != null)
        {
            if (!BoxingScrpit.isBlockingLeft)
            {
                StartCoroutine(TakeDamage());
            }
            else if (BoxingScrpit.isBlockingLeft)
            {
                StartCoroutine(TakeBlockDamage());
            }
        }
    }

    IEnumerator TakeDamage()
    {
        isTakingRightPunch = false;
        isTakingLeftPunch = false;
        Debug.Log("Punched");
        HP -= MaxDamage;

        // Apply knockback after taking damage
       ApplyKnockback();
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator TakeBlockDamage()
    {
        isTakingRightPunch = false;
        isTakingLeftPunch = false;
        Debug.Log("Blocked");
        HP -= BlockedDamage;

        // Apply knockback for blocked hits too (with less force if you want)
        yield return new WaitForSeconds(0);
    }
}