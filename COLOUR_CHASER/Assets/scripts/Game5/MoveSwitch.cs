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

    [Header("Audio")]
    [SerializeField] private AudioClip triggerSound;
    [SerializeField] private float soundVolume = 1f;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsValidPlayer(collision.tag) && targetPlatform != null)
        {
            targetPlatform.PressSwitch();

            if (triggerSound != null)
            {
                audioSource.PlayOneShot(triggerSound, soundVolume);
            }
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
