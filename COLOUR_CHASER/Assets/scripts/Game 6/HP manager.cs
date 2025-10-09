using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HPmanager : MonoBehaviour
{
    [SerializeField]
    private float hP, MaxHp;
    public Slider hpSlider;
    [SerializeField]
    private int ARDamage, SMGDamage, ShotGunDamage, PistolDamage, SniperDamage, BazookaDamage;
    private bool canRespawn;

    private GameObject[] SpawnPoints;
    [SerializeField]
    private float hpRestoreTime;
    [SerializeField]
    private int hpRestoreRate;
    [SerializeField]
    private int hpColldownTimer;
    public GameObject DeadPlayer1, DeadPlayer2;
    private PlayerInput playerInput;
    [SerializeField]
    private RawImage BG;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

    }
    private void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        if(playerInput.playerIndex ==0)
        {
            BG.color = new Color(0f,0f,1f, 0.5450981f);
        }
        else if (playerInput.playerIndex == 1)
        {
            BG.color = new Color(1f,0f,0f, 0.5450981f);
        }
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
                SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
                spriteRend.enabled = false;

                if (playerInput.playerIndex == 0)
                {
                    GameObject deadplayer = Instantiate(DeadPlayer1, transform.position, Quaternion.identity);
                    Destroy(deadplayer, 4);
                }
                else if (playerInput.playerIndex == 1)
                {
                    GameObject deadplayer = Instantiate(DeadPlayer2, transform.position, Quaternion.identity);
                    Destroy(deadplayer, 4);
                }
                Respawn();
                controls.ResetGuns();
                GameObject OldGun = controls.GunPoint.GetChild(0).gameObject;
                if (OldGun != null)
                {
                    Destroy(OldGun);
                }
            }
        }

        if (hpRestoreTime > 0)
        {
            hpRestoreTime -= Time.deltaTime;
        }

        Gainhp();
    }

    void Gainhp()
    {
        if (hpRestoreTime <= 0)
        {
            if (hP < MaxHp)
            {
                hP += Time.deltaTime * hpRestoreRate;
            }
        }
    }

    void Respawn()
    {
        StartCoroutine(RespawnPosition());
    }

    IEnumerator RespawnPosition()
    {
        yield return new WaitForSeconds(3);
        transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position;
        Game8playercontrols controls = GetComponent<Game8playercontrols>();
        controls.speed = 8;
        hP = MaxHp;
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.enabled = true;
        canRespawn = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ARBullet"))
        {
            hP -= ARDamage;
            Destroy(collision.gameObject);
            hpRestoreTime = hpColldownTimer;
        }
        else if (collision.collider.CompareTag("ShotGunBullet"))
        {
            hP -= ShotGunDamage;
            Destroy(collision.gameObject);
            hpRestoreTime = hpColldownTimer;
        }
        else if (collision.collider.CompareTag("SniperBullet"))
        {
            hP -= SniperDamage;
            Destroy(collision.gameObject);
            hpRestoreTime = hpColldownTimer;
        }
        else if (collision.collider.CompareTag("PistolBullet"))
        {
            hP -= PistolDamage;
            Destroy(collision.gameObject);
            hpRestoreTime = hpColldownTimer;
        }
        else if (collision.collider.CompareTag("BazookaBullet"))
        {
            hP -= BazookaDamage;
            Destroy(collision.gameObject);
            hpRestoreTime = hpColldownTimer;
        }
        else if (collision.collider.CompareTag("SMGBullet"))
        {
            hP -= SMGDamage;
            Destroy(collision.gameObject);
            hpRestoreTime = hpColldownTimer;
        }
    }
    

    IEnumerator LastShot()
    {
        yield return new WaitForSeconds(hpRestoreTime);
    }
    
}
