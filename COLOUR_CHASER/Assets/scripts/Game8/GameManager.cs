using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject joinPanel;
    [SerializeField] private TMP_Text player1Text;
    [SerializeField] private TMP_Text player2Text;

    [Header("Countdown")]
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float countdownTime = 3f;
    private int playersJoined = 0;

    private bool gameStarted = false;

    [Header("Score")]
    public int player1Score;
    public int player2Score;

    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;

    private void StartGame()
    {
        joinPanel.SetActive(false);
        StartCoroutine(CountdownRoutine());
    }

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 0f; 
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playersJoined++;

        if (playerInput.playerIndex == 0)
            player1Text.text = "Player 1: Joined ";
        else if (playerInput.playerIndex == 1)
            player2Text.text = "Player 2: Joined ";

 
        playerInput.GetComponent<TankController>().enabled = false;

        if (playersJoined >= 2)
            StartGame();
    }

    public void PlayerKilled(int deadPlayerIndex)
    {
        if (deadPlayerIndex == 0)
            player2Score++;
        else
            player1Score++;

        UpdateScoreUI();
        StartCoroutine(RespawnBothPlayers());
    }
    private void UpdateScoreUI()
    {
        player1ScoreText.text = $"P1: {player1Score}";
        player2ScoreText.text = $"P2: {player2Score}";
    }
    private IEnumerator RespawnBothPlayers()
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(0.5f);

        foreach (var tank in FindObjectsOfType<TankController>())
            tank.ResetToSpawn();

        Time.timeScale = 1f;
    }

    private IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        float timer = countdownTime;

        while (timer > 0)
        {
            countdownText.text = Mathf.Ceil(timer).ToString();
            yield return new WaitForSecondsRealtime(1f);
            timer--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(0.5f);

        countdownText.gameObject.SetActive(false);

        Time.timeScale = 1f;
        gameStarted = true;

        foreach (var tank in FindObjectsOfType<TankController>())
            tank.enabled = true;
    }

 
}
