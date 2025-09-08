using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointChecker : MonoBehaviour
{
    [SerializeField]
    private float countdownTimer = 60;
    [SerializeField]
    private int TimeRemaining;
    public TextMeshProUGUI TimerText;
    [SerializeField]
    public int Points;
    [SerializeField]
    public TextMeshProUGUI PointText1, PointText2;

    public void Update()
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            // Get integer seconds for display/logic
            TimeRemaining = Mathf.CeilToInt(Mathf.Max(0f, countdownTimer));
            TimerText.text = TimeRemaining.ToString();
        }

        PointText1.text = Points.ToString();
        PointText2.text = Points.ToString();

    }
}
