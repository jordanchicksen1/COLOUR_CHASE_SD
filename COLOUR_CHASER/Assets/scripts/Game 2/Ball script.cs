using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ballscript : MonoBehaviour
{
    private bool LasttouchPlayer1, LasttouchPlayer2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1"))
        {
            LasttouchPlayer1 = true;
            LasttouchPlayer2 = false;
        }
        else if (collision.collider.CompareTag("Player2"))
        {
            LasttouchPlayer1 = false;
            LasttouchPlayer2 = true;
        }
    }


}
