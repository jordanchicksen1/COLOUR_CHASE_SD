using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarGameManager : MonoBehaviour
{
    [Header("References")]
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    private int player1Score = 0;
    private int player2Score = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
         
            if (gameObject.tag == "GoalP1") 
            {
                player2Score++;
                player2ScoreText.text = player2Score.ToString();
            }
            else if (gameObject.tag == "GoalP2")
            {
                player1Score++;
                player1ScoreText.text = player1Score.ToString();
            }

            collision.gameObject.transform.position = Vector2.zero;
            collision.attachedRigidbody.velocity = Vector2.zero;
        }
    }
}
