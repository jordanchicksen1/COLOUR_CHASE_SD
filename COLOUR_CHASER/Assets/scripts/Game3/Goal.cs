using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GoalSide side;
    [SerializeField] private CarGameManager gameManager;

    private void Reset()
    {
        gameManager = FindObjectOfType<CarGameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ball")) return;

        if (!gameManager)
        {
            Debug.LogError($"Goal '{name}' has no GameManager assigned.");
            return;
        }

        gameManager.OnGoal(side, collision.rigidbody);
        Debug.Log($"GOAL by {(side == GoalSide.GoalP1 ? "Player 2" : "Player 1")} (hit {name})");
    }
}
