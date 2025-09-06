using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EndScene : MonoBehaviour
{
    [Header("Door Visuals (Separate Objects)")]
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private GameObject openDoor;

    [Header("Assigned Player Tag")]
    [SerializeField] private string assignedPlayerTag = "Player1"; // or "Player2"

    [Header("Scene Settings")]
#if UNITY_EDITOR
    [SerializeField] private SceneAsset nextScene; // drag & drop in Inspector
#endif
    [SerializeField, HideInInspector] private string nextSceneName;

    private readonly HashSet<Collider2D> overlaps = new HashSet<Collider2D>();

    private static bool player1Ready;
    private static bool player2Ready;
    private static bool sceneLoading;

    private void Awake()
    {
        // Reset static flags every time this scene is loaded
        player1Ready = false;
        player2Ready = false;
        sceneLoading = false;

        ShowClosed();

#if UNITY_EDITOR
        if (nextScene != null)
        {
            nextSceneName = nextScene.name;
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(assignedPlayerTag)) return;

        overlaps.Add(other);
        if (overlaps.Count > 0)
        {
            ShowOpen();
            SetReady(true);
            TryLoadNextScene();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(assignedPlayerTag)) return;

        overlaps.Remove(other);
        if (overlaps.Count == 0)
        {
            ShowClosed();
            SetReady(false);
        }
    }

    private void ShowOpen()
    {
        if (closedDoor && closedDoor.activeSelf) closedDoor.SetActive(false);
        if (openDoor && !openDoor.activeSelf) openDoor.SetActive(true);
    }

    private void ShowClosed()
    {
        if (closedDoor && !closedDoor.activeSelf) closedDoor.SetActive(true);
        if (openDoor && openDoor.activeSelf) openDoor.SetActive(false);
    }

    private void SetReady(bool ready)
    {
        if (assignedPlayerTag == "Player1") player1Ready = ready;
        else if (assignedPlayerTag == "Player2") player2Ready = ready;
    }

    private void TryLoadNextScene()
    {
        if (sceneLoading) return;

        if (player1Ready && player2Ready)
        {
            sceneLoading = true;
            Debug.Log("Both players ready! Loading next scene...");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}