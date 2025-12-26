using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowrUpPickup : MonoBehaviour
{
    public TankController.PowerUpType powerUpType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        TankController tank = other.GetComponent<TankController>();
        if (tank != null)
        {
            tank.GivePowerUp(powerUpType);
            Destroy(gameObject);
        }
    }
}
