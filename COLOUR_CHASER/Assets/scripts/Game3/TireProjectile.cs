using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // You can add logic to knockback cars or ball here
        Destroy(gameObject);
    }
}
