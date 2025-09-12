using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public float expandSpeed = 5f;
    public float maxSize = 5f;
    public float pushForce = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip shockwaveSound;
    [SerializeField] private float volume = 1f;

    void Start()
    {
        if (shockwaveSound != null)
        {
            AudioSource.PlayClipAtPoint(shockwaveSound, transform.position, volume);
        }
    }

    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

        if (transform.localScale.x >= maxSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            Vector2 dir = (other.transform.position - transform.position).normalized;
            rb.AddForce(dir * pushForce, ForceMode2D.Impulse);
        }
    }
}
