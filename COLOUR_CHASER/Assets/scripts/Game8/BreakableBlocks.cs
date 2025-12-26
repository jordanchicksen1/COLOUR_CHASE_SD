using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlocks : MonoBehaviour
{
    [SerializeField] private int hitsToBreak = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitsToBreak--;

            Destroy(collision.gameObject); 

            if (hitsToBreak <= 0)
                Destroy(gameObject);
        }
    }
}
