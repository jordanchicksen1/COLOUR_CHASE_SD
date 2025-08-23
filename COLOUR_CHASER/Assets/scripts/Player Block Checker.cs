using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockChecker : MonoBehaviour
{
    private SpriteRenderer sprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Right"))
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.green;
        }
    }
}
