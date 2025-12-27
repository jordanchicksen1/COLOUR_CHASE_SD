using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int ownerIndex;

    public void SetOwner(int index)
    {
        ownerIndex = index;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TankController tank))
        {
            if (tank.PlayerIndex != ownerIndex)
            {
                tank.Die();
                GameManager.Instance.PlayerKilled(tank.PlayerIndex);
            }
        }

        Destroy(gameObject);
    }
}
