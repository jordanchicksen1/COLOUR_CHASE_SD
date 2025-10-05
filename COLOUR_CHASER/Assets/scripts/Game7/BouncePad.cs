using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2"))
            {
                Vector2 velocity = rb.velocity;
                velocity.y = 0;
                rb.velocity = velocity;

                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
