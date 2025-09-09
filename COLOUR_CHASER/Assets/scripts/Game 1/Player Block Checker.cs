using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBlockChecker : MonoBehaviour
{
    public List<bool> Correctblock;
    public RandomBlockAssigner assignerScript;
    [SerializeField]
    private float countdownTimer = 50;
    [SerializeField]
    private int TimeRemaining;
    public TextMeshProUGUI TimerText;
    private bool CanCheckForGame;
    public SpriteRenderer[] spriteRenderers;
    public GameObject GameOverPanel, WinPanel;
    public GameObject BGMusic;
    private void Update()
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            // Get integer seconds for display/logic
            TimeRemaining = Mathf.CeilToInt(Mathf.Max(0f, countdownTimer));
            TimerText.text = TimeRemaining.ToString();
        }
        
        for(int i = 0; i < spriteRenderers.Length; i++)
        {
            if (Correctblock[i] == true)
            {
               spriteRenderers[i].color = Color.green;
            }
            else
            {
                spriteRenderers[i].color = Color.red;

            }
        }

        

        if (TimeRemaining == 0)
        {
            if (CanCheckForGame)
            {
                CanCheckForGame = false;
                if (Correctblock.All(x => x))
                {
                    StartCoroutine(assignerScript.AssignBlocks());
                    for (int i = 0; i < 2; i++)
                    {
                        Correctblock[i] = false;
                    }
                }
                else
                {
                   GameOverPanel.SetActive(true);
                    BGMusic.SetActive(false);
                }
            }
        }
    }

    public void StartTimer()
    {
        countdownTimer = 5;
        CanCheckForGame = true;

    }

}
