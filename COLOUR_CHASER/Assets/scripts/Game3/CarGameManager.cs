using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GoalSide { GoalP1, GoalP2 }

public class CarGameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;
    [SerializeField] private TMP_Text countdownText;

    private int player1Score;
    private int player2Score;

    [Header("Ball")]
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private Vector2 kickoffPoint = Vector2.zero;
    [SerializeField] private float countdownTime = 3f;

    private bool roundActive = false;
    private int playersJoined = 0;

    private void Start()
    {
        if (ball) ball.simulated = false;
        countdownText.text = "Waiting for Players...";
    }

    public void RegisterPlayer()
    {
        playersJoined++;
        if (playersJoined == 2)
        {
            StartCoroutine(StartCountdown());
        }
    }

    public void OnGoal(GoalSide side, Rigidbody2D ballRb)
    {
        if (side == GoalSide.GoalP1) player2Score++;
        else player1Score++;

        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        roundActive = false;

        ball.velocity = Vector2.zero;
        ball.angularVelocity = 0f;
        ball.simulated = false;
        ball.transform.position = kickoffPoint;

        for (int i = (int)countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        ball.simulated = true;
        roundActive = true;
    }
}