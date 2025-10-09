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

        Vector2 contactPoint = collision.GetContact(0).point;

        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                float directionX = Mathf.Sign(collision.transform.position.x - transform.position.x);
                playerRb.velocity = new Vector2(directionX * knockbackForce, 0f);
            }
        }

        PlayImpactEffect(contactPoint);
    }

    void PlayImpactEffect(Vector2 position)
    {
        if (impactEffect != null)
        {
            ParticleSystem effect = Instantiate(impactEffect, position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }

        Destroy(gameObject);
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
