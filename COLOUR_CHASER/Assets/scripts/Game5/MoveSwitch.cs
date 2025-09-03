using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSwitch : MonoBehaviour
{
    [Header("Switch Settings")]
    [SerializeField] private bool reactsToPlayer1 = true;
    [SerializeField] private bool reactsToPlayer2 = true;

    [Header("Target Platform")]
    [SerializeField] private PlatformController targetPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsValidPlayer(collision.tag) && targetPlatform != null)
        {
            targetPlatform.PressSwitch();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsValidPlayer(collision.tag) && targetPlatform != null)
        {
            targetPlatform.ReleaseSwitch();
        }
    }

    private bool IsValidPlayer(string tag)
    {
        return (reactsToPlayer1 && tag == "Player1") || (reactsToPlayer2 && tag == "Player2");
    }
}
