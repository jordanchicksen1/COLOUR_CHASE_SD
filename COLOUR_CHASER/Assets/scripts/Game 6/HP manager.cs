using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPmanager : MonoBehaviour
{
    [SerializeField]
    private int Hp, MaxHp;
    [SerializeField]


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ARBullet"))
        {

        }
        else if (collision.collider.CompareTag("ShotGunBullet"))
        {

        }
        else if (collision.collider.CompareTag("SniperBullet"))
        {

        }
        else if (collision.collider.CompareTag("PistolBullet"))
        {

        }
        else if (collision.collider.CompareTag("BazookaBullet"))
        {

        }
        else if (collision.collider.CompareTag("SMGBullet"))
        {

        }
    }
}
