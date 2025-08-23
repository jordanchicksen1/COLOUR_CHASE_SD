using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RandomBlockAssigner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Blocks;
    [SerializeField]
    private string Right = "Right";
    public List<Color> RandomColour;     // A nice sky blue
    private GameObject ChosenBlock1,ChosenBlock2;
    public TextMeshProUGUI ColourText;
    [SerializeField]
    private string[] Colours;
    private Color ChosenColour;
    [SerializeField]
    private TextMeshProUGUI TimerText;
    public GameObject TimerPanel;
    public GameObject ChosenColourPanel;
    private void Start()
    {
        Blocks = GameObject.FindGameObjectsWithTag("Block");
        RandomColour.Add(new Color(0.2f, 0.6f, 1f));    // Sky Blue / Azure Blue
        RandomColour.Add(new Color(1f, 0.4f, 0.8f));    // Bubblegum Pink / Hot Pink
        RandomColour.Add(new Color(1f, 0.92f, 0.2f));   // Bright Yellow / Lemon Yellow
        RandomColour.Add(new Color(0.4f, 0.8f, 0.2f));  // Lime Green / Apple Green
        RandomColour.Add(new Color(0.7f, 0.3f, 1f));    // Electric Purple / Amethyst Purple
        RandomColour.Add(new Color(1f, 0.3f, 0.3f));    // Coral Red / Tomato Red

    }

    public void AssignBlockCode()
    {
        TimerPanel.SetActive(false);

        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i].tag = "Block";
            Blocks[i].name = "Block";

        }

        for (int i = 0; i < 1; i++)
        {
            ChosenBlock1 = Blocks[Random.Range(0, Blocks.Length)];
            ChosenBlock2 = Blocks[Random.Range(0, Blocks.Length)];
            if (ChosenBlock2 == ChosenBlock1)
            {
               ChosenBlock2 = Blocks[Random.Range(0, Blocks.Length)];
            }
            ChosenBlock1.tag = Right;
            ChosenBlock2.tag = Right;

            SpriteRenderer spriteRenderer = ChosenBlock1.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer2 = ChosenBlock2.GetComponent<SpriteRenderer>();

            spriteRenderer.color = ChosenColour;
            spriteRenderer2.color = ChosenColour;

        }

        for (int p = 0; p < Blocks.Length; p++)
        {
            if (Blocks[p].tag != Right)
            {
                SpriteRenderer spriteRenderer = Blocks[p].GetComponent<SpriteRenderer>();
                Color randomColor;

                // Keep generating random colors until we get one that's not the ChosenColour
                do
                {
                    randomColor = RandomColour[Random.Range(0, RandomColour.Count)];
                }
                while (randomColor == ChosenColour);

                spriteRenderer.color = randomColor;
            }
        }
    }

    IEnumerator AssignBlocks()
    {
        TimerPanel.SetActive(true);
        ChosenColourPanel.SetActive(true);

        ChosenColour = RandomColour[Random.Range(0, RandomColour.Count)];
        ColourText.color = ChosenColour;

        yield return new WaitForSeconds(2);
        ChosenColourPanel.SetActive(false);
        TimerText.color = ChosenColour;
        TimerText.text = "3";
        yield return new WaitForSeconds(1);
        TimerText.text = "2";
        yield return new WaitForSeconds(1);
        TimerText.text = "1";
        yield return new WaitForSeconds(1);
        TimerText.text = "";

        AssignBlockCode();


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AssignBlocks());
        }

        if (ChosenColour == RandomColour[0])
        {
            ColourText.text = Colours[0];
        }
        else if (ChosenColour == RandomColour[1])
        {
            ColourText.text = Colours[1];

        }
        else if (ChosenColour == RandomColour[2])
        {
            ColourText.text = Colours[2];

        }
        else if (ChosenColour == RandomColour[3])
        {
            ColourText.text = Colours[3];

        }
        else if (ChosenColour == RandomColour[4])
        {
            ColourText.text = Colours[4];

        }
        else if (ChosenColour == RandomColour[5])
        {
            ColourText.text = Colours[5];

        }
    }
}
