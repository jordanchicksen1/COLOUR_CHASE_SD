using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsFruit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultsScoreText;
    [SerializeField] private float countSpeed = 1f; // smaller = faster
    [SerializeField] private AudioSource countSound;  // optional: little ticking sound

    private void Start()
    {
        StartCoroutine(AnimateFinalScore());
    }

    private IEnumerator AnimateFinalScore()
    {
        int finalScore = GameData.playerScore;
        int displayedScore = 0;

        while (displayedScore < finalScore)
        {
            displayedScore++;
            resultsScoreText.text = displayedScore.ToString();

            if (countSound != null)
                countSound.Play();

            yield return new WaitForSeconds(countSpeed);
        }

        // Reset after showing the total
        GameData.playerScore = 0;
    }
}
