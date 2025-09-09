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
    public TextMeshProUGUI PointText1, PointText2, FinalScore;
    public GameObject GameOverPanel;
    public GameObject FruitSpawner;
    GameObject[] Fruit;


    public void Update()
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            // Get integer seconds for display/logic
            TimeRemaining = Mathf.CeilToInt(Mathf.Max(0f, countdownTimer));
            TimerText.text = TimeRemaining.ToString();
        }
        else if (TimeRemaining == 0)
        {
            GameOverPanel.SetActive(true);
            FruitSpawner.SetActive(false);
            Fruit = GameObject.FindGameObjectsWithTag("Apple");
            foreach (GameObject obj in Fruit)
            {
                Destroy(obj);
            }
        }

        PointText1.text = Points.ToString();
        PointText2.text = Points.ToString();
        FinalScore.text = Points.ToString();
    }
}
