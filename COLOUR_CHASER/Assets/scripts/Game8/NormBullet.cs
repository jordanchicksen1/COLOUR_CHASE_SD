using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;
    private int bouncesLeft = 1;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (bouncesLeft > 0)
        {
            Vector2 reflect = Vector2.Reflect(rb.velocity, col.contacts[0].normal);
            rb.velocity = reflect;
            bouncesLeft--;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
