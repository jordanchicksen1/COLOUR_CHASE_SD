using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ballscript : MonoBehaviour
{
    private bool LasttouchPlayer1, LasttouchPlayer2;
    [SerializeField]
    private int Player1Score, Player2Score;
    public TextMeshProUGUI Player1ScoreText, Player2ScoreText, StartTimerText;
    private Vector2 OriginalPosition;
    public PlayerInputManager inputManager;
    private bool GamePlaying;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRnd;

    private void Start()
    {
        OriginalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        spriteRnd = GetComponent<SpriteRenderer>();
        rb.isKinematic = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1"))
        {

            LasttouchPlayer1 = false;
            LasttouchPlayer2 = true;
        }
        else if (collision.collider.CompareTag("Player2"))
        {
            LasttouchPlayer1 = true;
            LasttouchPlayer2 = false;
        }
        else if (collision.collider.CompareTag("Floor"))
        {
            if (LasttouchPlayer1)
            {
                Player1Score--;
                StartCoroutine(StartGame());
            }
            else if (LasttouchPlayer2)
            {
                Player2Score--;
                StartCoroutine(StartGame());

            }
        }
    }

    private void Update()
    {
        Player1ScoreText.text = Player1Score.ToString();
        Player2ScoreText.text = Player2Score.ToString();
        if (inputManager.playerCount ==2 && !GamePlaying)
        {
            GamePlaying = true;
            StartCoroutine(StartGame());

        }
    }

    IEnumerator StartGame()
    {
        LasttouchPlayer1 = false;
        LasttouchPlayer2 = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.position = OriginalPosition;
        spriteRnd.color = Color.white;
        StartTimerText.text = "3";
        yield return new WaitForSeconds(1);
        StartTimerText.text = "2";
        yield return new WaitForSeconds(1);
        StartTimerText.text = "1";
        yield return new WaitForSeconds(1);
        StartTimerText.text = "Start";
        yield return new WaitForSeconds(1);
        StartTimerText.text = "";
        rb.isKinematic = false;
    }

}
