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
        ToggleBarrier(collision.collider.tag);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ToggleBarrier(collision.collider.tag);
    }

    private void ToggleBarrier(string tag)
    {
        if ((blocksPlayer1 && tag == "Player1") || (blocksPlayer2 && tag == "Player2"))
        {
            col.isTrigger = false; // block this player
        }
        else
        {
            col.isTrigger = true; // let other player pass
        }
    }
}
