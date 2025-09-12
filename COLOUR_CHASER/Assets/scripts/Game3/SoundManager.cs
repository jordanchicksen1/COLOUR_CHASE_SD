using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Ability Sounds")]
    public AudioSource tireSource;
    public AudioSource oilSource;
    public AudioSource shockwaveSource;
    public AudioSource speedSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
