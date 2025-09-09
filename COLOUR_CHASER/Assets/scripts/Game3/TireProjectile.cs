using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireProjectile : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 3f;
    public float pushForce = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float volume = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position, volume);

        Rigidbody2D targetRb = collision.collider.GetComponent<Rigidbody2D>();
        if (targetRb != null)
        {
            Vector2 pushDir = rb.velocity.normalized;
            targetRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
        }

        if (collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2"))
        {
            Destroy(gameObject);
        }
    }
}
