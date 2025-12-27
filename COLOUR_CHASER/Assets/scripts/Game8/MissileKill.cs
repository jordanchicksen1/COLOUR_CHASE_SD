using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileKill : MonoBehaviour
{
    private int ownerIndex;
    // Call this when spawning the missile
    public void SetOwner(int index)
    {
        ownerIndex = index;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TankController tank = collision.GetComponent<TankController>();
        if (tank != null && tank.PlayerIndex != ownerIndex)
        {
            tank.Die();
        }

        GameManager.Instance.PlayerKilled(tank.PlayerIndex);

        Destroy(gameObject);
    }
}

