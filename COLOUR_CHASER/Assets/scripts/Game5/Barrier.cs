using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [Header("Barrier Settings")]
    [SerializeField] private bool blocksPlayer1 = true;
    [SerializeField] private bool blocksPlayer2 = true;

    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.collider);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollision(collision.collider);
    }

    private void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Player1") && !blocksPlayer1)
        {
            Physics2D.IgnoreCollision(other, col, true);
        }
        else if (other.CompareTag("Player2") && !blocksPlayer2)
        {
            Physics2D.IgnoreCollision(other, col, true);
        }
        else
        {
            Physics2D.IgnoreCollision(other, col, false);
        }
    }
}
