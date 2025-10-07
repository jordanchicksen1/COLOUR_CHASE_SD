using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPmanager : MonoBehaviour
{
    [SerializeField]
    private int hP, MaxHp;
    public Slider hpSlider;
    [SerializeField]
    private int ARDamage, SMGDamage, ShotGunDamage, PistolDamage, SniperDamage, BazookaDamage;
    private bool gotShot;
    private float TimeFrombeingShot;

    private void Update()
    {
        hpSlider.value = hP;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ARBullet"))
        {
            hP -= ARDamage;
            Destroy(collision.gameObject);
            gotShot = true;
        }
        else if (collision.collider.CompareTag("ShotGunBullet"))
        {
            hP -= ShotGunDamage;
            Destroy(collision.gameObject);
            gotShot = true;
        }
        else if (collision.collider.CompareTag("SniperBullet"))
        {
            hP -= SniperDamage;
            Destroy(collision.gameObject);
            gotShot = true;
        }
        else if (collision.collider.CompareTag("PistolBullet"))
        {
            hP -= PistolDamage;
            Destroy(collision.gameObject);
            gotShot = true;
        }
        else if (collision.collider.CompareTag("BazookaBullet"))
        {
            hP -= BazookaDamage;
            Destroy(collision.gameObject);
            gotShot = true;
        }
        else if (collision.collider.CompareTag("SMGBullet"))
        {
            hP -= SMGDamage;
            Destroy(collision.gameObject);
            gotShot = true;
        }
    }
    
    
}
