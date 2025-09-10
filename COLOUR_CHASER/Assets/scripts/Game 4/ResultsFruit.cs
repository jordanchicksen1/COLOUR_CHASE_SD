using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsFruit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultsScoreText;

    void Start()
    {
        resultsScoreText.text = "" + GameData.playerScore;

        // reset so it doesn’t carry over
        GameData.playerScore = 0;
    }
}
