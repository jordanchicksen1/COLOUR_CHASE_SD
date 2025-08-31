using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField]
    private bool MoveUp, MoveDown;
    private Rigidbody2D rb;
    [SerializeField]
    private int moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Up"))
        {
            MoveUp = true;
        }
        else if (collision.CompareTag("Down"))
        {
            MoveDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Up"))
        {
            MoveUp = false;
        }
        else if (collision.CompareTag("Down"))
        {
            MoveDown = false;
        }
    }

    private void Update()
    {
        if (MoveUp)
        {
            if (transform.parent == null)
            {
                rb.velocity = Vector2.up * moveSpeed * Time.deltaTime;
            }
        }
        else if(MoveDown)
        {
            if (transform.parent == null)
            {
                rb.velocity = Vector2.down * moveSpeed * Time.deltaTime;
            }
        }
    }
}
