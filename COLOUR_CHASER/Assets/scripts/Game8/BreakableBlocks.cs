using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlocks : MonoBehaviour
{
    [Header("Block Health")]
    [SerializeField] private int hitsToBreak = 3;

    [Header("Damage Sprites")]
    [SerializeField] private Sprite crackedSprite;
    [SerializeField] private Sprite veryCrackedSprite;

    private SpriteRenderer sr;
    private int hitsTaken = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
            return;

        Destroy(collision.gameObject);

        hitsTaken++;

        if (hitsTaken == 1)
        {
            sr.sprite = crackedSprite;
        }
        else if (hitsTaken == 2)
        {
            sr.sprite = veryCrackedSprite;
        }
        else if (hitsTaken >= hitsToBreak)
        {
            Destroy(gameObject);
        }
    }
}
