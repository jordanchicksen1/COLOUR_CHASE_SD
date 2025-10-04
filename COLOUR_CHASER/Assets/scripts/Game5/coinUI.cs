using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class coinUI : MonoBehaviour
{
    public TextMeshProUGUI coinUIText;

    void Start()
    {
        UpdateCoinText();
    }

    void Update()
    {
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        if (coinUIText != null)
            coinUIText.text = GameData.coinScore.ToString();
    }
}
