using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireProjectile : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 3f;
    public float pushForce = 10f; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.tag;

        if (tag == "Player1" || tag == "Player2" || tag == "Ball")
        {
            Rigidbody2D targetRb = collision.collider.GetComponent<Rigidbody2D>();
            if (targetRb != null)
            {
                Vector2 pushDir = rb.velocity.normalized;
                targetRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
            }

            Destroy(gameObject);
        }
    }

}
