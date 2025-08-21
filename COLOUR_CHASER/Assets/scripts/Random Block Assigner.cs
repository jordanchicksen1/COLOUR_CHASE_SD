using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i].tag = "Block";
            Blocks[i].name = "Block";

        }

        Color ChosenColor = RandomColour[Random.Range(0, RandomColour.Count)];

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

            ChosenBlock1.name = "Chosen";
            ChosenBlock2.name = "Chosen";

            SpriteRenderer spriteRenderer = ChosenBlock1.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer2 = ChosenBlock2.GetComponent<SpriteRenderer>();

            spriteRenderer.color = ChosenColor;
            spriteRenderer2.color = ChosenColor;

        }

        for (int p = 0; p < Blocks.Length; p++)
        {
            SpriteRenderer spriteRenderer = Blocks[p].GetComponent<SpriteRenderer>();
            if (Blocks[p].tag != Right)
            {
                spriteRenderer.color = RandomColour[Random.Range(0, RandomColour.Count)];
                if (spriteRenderer.color == ChosenColor)
                {
                    p= 0; 
                }
             
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AssignBlockCode();
        }
    }
}
