using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float speed = 6f; 
    [SerializeField] private float lifetime = 3f; 
    private Rigidbody2D rb; void Awake() { rb = GetComponent<Rigidbody2D>(); }
    void Start() { rb.velocity = transform.up * speed; Destroy(gameObject, lifetime); }
}
