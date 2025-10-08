using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Header("Match Settings")]
    public float matchDuration = 180f;

    [Header("UI Reference (Optional)")]
    public TMP_Text timerText;

    private float timeRemaining;
    private bool matchEnded = false;

    void Start()
    {
        timeRemaining = matchDuration;
    }

    void Update()
    {
        if (matchEnded) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndMatch();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    void EndMatch()
    {
        matchEnded = true;

        int p1 = ScoreManager.Instance.player1Score;
        int p2 = ScoreManager.Instance.player2Score;

        if (p1 > p2)
        {
            SceneManager.LoadScene("G7P1Win");
        }
        else if (p2 > p1)
        {
            SceneManager.LoadScene("G7P2Win");
        }
        else
        {
            SceneManager.LoadScene("G7Draw");
        }
    }
}
