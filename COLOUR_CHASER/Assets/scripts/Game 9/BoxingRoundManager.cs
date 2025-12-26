using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxingRoundManager : MonoBehaviour
{
    public int currentRound;
    [SerializeField]
    private List<RawImage> Player1Rounds, Player2Rounds;
    [SerializeField]
    public int Player1WinIndex, Player2WinIndex;
    [SerializeField]
    private PlayerInputManager playerInputManager;
    private GameObject Player1, Player2;
    
    private void Update()
    {
        if(Player1WinIndex >= 0)
        {
            Player1Rounds[Player1WinIndex].color = Color.blue;
        }

        if (Player2WinIndex >= 0)
        {
            Player2Rounds[Player2WinIndex].color = Color.red;
        }

        if (Player1WinIndex == 1)
        {
            //Boxing Player 1 Win
            SceneManager.LoadScene("P1Boxing");
        }
        else if (Player2WinIndex ==1)
        {
            //Boxing Player 1 Win

            SceneManager.LoadScene("P2Boxing");

        }
    }
}
