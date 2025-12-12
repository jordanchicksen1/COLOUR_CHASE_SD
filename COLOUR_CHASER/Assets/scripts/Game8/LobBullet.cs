using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobBullet : MonoBehaviour
{
    public float lifetime = 1.2f;
    public float slowDown = 4f;

    private Rigidbody2D rb;

    public float startScale = 0.7f;
    public float peakScale = 1.4f;

    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = Vector3.one * startScale;

        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        float t = timer / lifetime;

        float curve = 1f - Mathf.Abs((t * 2f) - 1f);

        float scale = Mathf.Lerp(startScale, peakScale, curve);
        transform.localScale = new Vector3(scale, scale, 1f);

        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, slowDown * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
