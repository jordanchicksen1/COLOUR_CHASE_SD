using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGame7 : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 3f;
    public float knockbackForce = 10f;

    [Header("References")]
    public ParticleSystem impactEffect;

    private Rigidbody2D rb;
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = transform.right.normalized * speed;

        Invoke(nameof(DestroyBullet), lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHit) return;
        hasHit = true;

        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockDir = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
            }
        }

        PlayImpactEffect();
    }

    void DestroyBullet()
    {
        if (!hasHit)
            PlayImpactEffect();
    }

    void PlayImpactEffect()
    {
        if (impactEffect != null)
        {
            impactEffect.transform.parent = null;
            impactEffect.Play();
            Destroy(impactEffect.gameObject, impactEffect.main.duration);
        }

        Destroy(gameObject);
    }
}
