using System.Collections;
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
    private void Start()
    {
        BoxingScrpit = GetComponent<BoxingController>();
        rb = GetComponent<Rigidbody2D>();
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

    IEnumerator KnockbackRoutine()
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