using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int player1Score = 0;
    public int player2Score = 0;

    [Header("UI References")]
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScoreToPlayer1()
    {
        GameData.playerOneScore++;
        player1Score++;
        UpdateUI();
    }

    public void AddScoreToPlayer2()
    {
        GameData.playerTwoScore++;
        player2Score++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (player1ScoreText != null)
            player1ScoreText.text = "" + player1Score;
        if (player2ScoreText != null)
            player2ScoreText.text = "" + player2Score;
    }
}
