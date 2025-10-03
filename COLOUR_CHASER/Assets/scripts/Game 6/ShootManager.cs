using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
   private Game8playercontrols playercontrols;

    private void Start()
    {
        playercontrols = GetComponent<Game8playercontrols>();
    }
}
