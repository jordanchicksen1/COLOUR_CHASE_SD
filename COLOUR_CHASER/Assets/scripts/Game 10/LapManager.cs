using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LapManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Player1CheckPoints, Player2CheckPoints;
    private int Player1Index, Player2Index;

    [SerializeField]
    private int player1Laps, player2Laps;

    public TextMeshProUGUI player1Text, player2Text;
    public AudioSource p1LapSFX;
    public AudioSource p2LapSFX;

    [Header("Race Start")]
    [SerializeField] private int requiredPlayers = 2;
    private int playersReady = 0;

    private RacingController[] racers;

    [SerializeField] private GameObject panel3;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panelGo;
    public GameObject backgroundMusic;

    [Header("Racer Chooser")]
    [SerializeField] private bool Race1 = false;
    [SerializeField] private bool Race2 = false;
    [SerializeField] private bool Race3 = false;

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
            p1LapSFX.Play();
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
            p2LapSFX.Play();
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

    public void Update()
    {
        if (player1Laps == 4 && Race1 == true)
        {
            //Win Code
            SceneManager.LoadScene("P1Race1");
        }
        else if(player2Laps == 4 && Race1 == true)
        {
            //Win Code
            SceneManager.LoadScene("P2Race1");
        }

        player1Text.text = player1Laps.ToString();
        player2Text.text = player2Laps.ToString();
    }

    public void RegisterPlayer(RacingController racer)
    {
        playersReady++;

        if (playersReady >= requiredPlayers)
        {
            StartCoroutine(GameCounter());
        }
    }

    private IEnumerator GameCounter()
    {
        racers = FindObjectsOfType<RacingController>();

        yield return new WaitForSeconds(0f);
        panel3.SetActive(true);

        yield return new WaitForSeconds(1f);
        panel3.SetActive(false);
        panel2.SetActive(true);

        yield return new WaitForSeconds(1f);
        panel2.SetActive(false);
        panel1.SetActive(true);

        yield return new WaitForSeconds(1f);
        panel1.SetActive(false);
        panelGo.SetActive(true);

        yield return new WaitForSeconds(1f);
        panelGo.SetActive(false);
        backgroundMusic.SetActive(true);
        
        foreach (var racer in racers)
        {
            racer.canDrive = true;
        }
    }

}
