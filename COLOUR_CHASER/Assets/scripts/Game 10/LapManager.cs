using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Player1CheckPoints, Player2CheckPoints;
    private int Player1Index, Player2Index;

    [SerializeField]
    private int player1Laps, player2Laps;

    public void ActivateNextCheckPoint1()
    {
        if (Player1Index == Player1CheckPoints.Count)
        {
            for (int i = 0; i < Player1CheckPoints.Count; i++)
            {
                Player1CheckPoints[i].SetActive(false);
            }
            Player1Index = 0;
            player1Laps++;
            Player1CheckPoints[Player1Index].SetActive(true);
        }
        else
        {
            for (int i = 0; i < Player1CheckPoints.Count; i++)
            {
                Player1CheckPoints[i].SetActive(false);
            }
            Player1CheckPoints[Player1Index].SetActive(true);
            Player1Index++;
        }
    }

    public void ActivateNextCheckPoint2()
    {
        if (Player2Index == Player2CheckPoints.Count )
        {
            for (int i = 0; i < Player2CheckPoints.Count; i++)
            {
                Player2CheckPoints[i].SetActive(false);
            }
            Player2Index = 0;
            player2Laps++;
            Player2CheckPoints[Player2Index].SetActive(true);
        }
        else
        {
            for (int i = 0; i < Player2CheckPoints.Count; i++)
            {
                Player2CheckPoints[i].SetActive(false);
            }
            Player2CheckPoints[Player2Index].SetActive(true);
            Player2Index++;
        }
    }
}
