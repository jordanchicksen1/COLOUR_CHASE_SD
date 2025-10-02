using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class coinsResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultsScoreText;

    void Start()
    {
        resultsScoreText.text = "" + GameData.coinScore;

        // reset so it doesn’t carry over
        GameData.coinScore = 0;
    }
}
