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

    private int playersJoined = 0;

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

    private void StartGame()
    {
        joinPanel.SetActive(false);
        Time.timeScale = 1f;

        foreach (var tank in FindObjectsOfType<TankController>())
            tank.enabled = true;
    }
}
