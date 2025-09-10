using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Ballscript : MonoBehaviour
{
    [SerializeField]
    private bool LasttouchPlayer1, LasttouchPlayer2;
    [SerializeField]
    private int Player1Score, Player2Score;
    public TextMeshProUGUI Player1ScoreText, Player2ScoreText, StartTimerText;
    private Vector2 OriginalPosition;
    public PlayerInputManager inputManager;
    private bool GamePlaying;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRnd;
    public Animator animator;
    public GameObject Player1Wins, Player2Wins;
    public GameObject MusicSource;
    public AudioSource Score;
    private void Start()
    {
        OriginalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        spriteRnd = GetComponent<SpriteRenderer>();
        rb.isKinematic = true;
        MusicSource.SetActive(true);

    }

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
        else if (collision.collider.CompareTag("Floor"))
        {
            if (spriteRnd.color == Color.red)
            {
                Player2Score--;
                Debug.Log("Last player was player 1");
                StartCoroutine(StartGame());
                StartCoroutine(ShakeCamera());
                Score.Play();
            }
            else if (spriteRnd.color == Color.blue)
            {
                Player1Score--;
                Debug.Log("Last player was player 2");

                StartCoroutine(StartGame());
                StartCoroutine(ShakeCamera());
                Score.Play();

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

        if (LasttouchPlayer1 && LasttouchPlayer2)
        {
            spriteRnd.color = Color.white;
        }

        if (Player1Score == 0)
        {
            StartCoroutine(Player2Won());

        }
        else if (Player2Score == 0)
        {
            StartCoroutine(Player1Won());

        }
    }
    
    IEnumerator ShakeCamera()
    {
        animator.SetBool("Shake", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Shake", false);

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

    public IEnumerator Player1Won()
    {
        yield return new WaitForSeconds(0.5f);
        
        SceneManager.LoadScene("P1Balloon");
       
    }

    public IEnumerator Player2Won()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("P2Balloon");

    }

}
