using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBlockChecker : MonoBehaviour
{
    public List<bool> Correctblock;

    private void Update()
    {
        if (Correctblock.All(x => x))
        {

        }
    }
}
