using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            ScoreManager.Instance.AddScoreToPlayer2();
        }
        else if (collision.CompareTag("Player2"))
        {
            ScoreManager.Instance.AddScoreToPlayer1();
        }

        BrawlerController player = collision.GetComponent<BrawlerController>();
        if (player != null)
        {
            player.DropOrRemoveWeapon();

            player.RespawnToSpawn();
        }
    }
}
