using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
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
    private bool canRespawn;

    private GameObject[] SpawnPoints;

    private void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
    }
    private void Update()
    {
        hpSlider.value = hP;
        if (hP <= 0)
        {
            if (!canRespawn)
            {
                canRespawn = true;
                Game8playercontrols controls = GetComponent<Game8playercontrols>();
                controls.speed = 0;
                Respawn();
            }
        }
    }

    void Respawn()
    {
        StartCoroutine(RespawnPosition());
    }

    IEnumerator RespawnPosition()
    {
        yield return new WaitForSeconds(2);
        transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position;
        Game8playercontrols controls = GetComponent<Game8playercontrols>();
        controls.speed = 5;
        hP = MaxHp;
        canRespawn = false;
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
