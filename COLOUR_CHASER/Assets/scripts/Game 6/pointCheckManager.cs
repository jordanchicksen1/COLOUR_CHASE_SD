using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pointCheckManager : MonoBehaviour
{
    public int PLayer1Score, PLayer2Score;

    private void Update()
    {
        if(PLayer1Score == 5)
        {
            SceneManager.LoadScene("p1WinsGame6");
        }
        else if (PLayer2Score == 5)
        {
            SceneManager.LoadScene("p2WinsGame6");
        }

    }
}
