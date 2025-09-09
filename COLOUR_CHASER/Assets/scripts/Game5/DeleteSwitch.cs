using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSwitch : MonoBehaviour
{
    [Header("Switch Settings")]
    [SerializeField] private bool reactsToPlayer1 = true;
    [SerializeField] private bool reactsToPlayer2 = true;

    [Header("Object To Delete")]
    [SerializeField] private GameObject objectToDelete;

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
        if (IsValidPlayer(collision.tag))
        {
            if (triggerSound != null)
            {
                audioSource.PlayOneShot(triggerSound, soundVolume);
            }

            if (objectToDelete != null)
            {
                Destroy(objectToDelete);
            }
        }
    }

    private bool IsValidPlayer(string tag)
    {
        return (reactsToPlayer1 && tag == "Player1") || (reactsToPlayer2 && tag == "Player2");
    }
}
