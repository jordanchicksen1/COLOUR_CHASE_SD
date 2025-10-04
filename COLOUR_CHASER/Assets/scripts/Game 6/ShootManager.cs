using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public bool isShooting;
    public List<bool> Guns;
    [SerializeField]
    private int shootRate;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletPoint;
    [SerializeField]
    private int bulletSpeed;
    public bool isRunning;
    private bool isReLoading;
    [SerializeField]
    private int ReloadTime;
    //Guns[0] = sniper
    //Guns[1] = ShotGun
    //Guns[2] = SMG
    //Guns[3] = AR
    //Guns[4] = pistol
    //Guns[5] = laser
    //Guns[6] = bazooka

    private void Update()
    {
       if (isShooting)
        {
            if (!isRunning)
            {
                isRunning = true;
                if (Guns[0])
                {
                    StartCoroutine(SniperShoot());
                }
                else if (Guns[1])
                {
                    StartCoroutine(ShotGunShoot());
                }
                else if (Guns[2])
                {
                    StartCoroutine(SMGShoot());
                }
                else if (Guns[3])
                {
                    StartCoroutine(ARShoot());
                }
                else if (Guns[4])
                {
                    StartCoroutine(PistolShoot());
                }
                else if (Guns[5])
                {
                    StartCoroutine(LaserShoot());
                }
                else if (Guns[6])
                {
                    StartCoroutine(BazookaShoot());
                }
            }
        }
    }

    IEnumerator SniperShoot()
    {
        if(!isReLoading)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * bulletSpeed;
            Destroy(bullet, 0.5f);
            StartCoroutine(ReloadChecker());
            yield return new WaitForSeconds(shootRate);

            if (isShooting)
            {
                StartCoroutine(SniperShoot());
            }
        }
    }

    IEnumerator ReloadChecker()
    {
        isReLoading = true;
        yield return new WaitForSeconds(ReloadTime);
        isReLoading = false;

    }

    IEnumerator ShotGunShoot()
    {
        yield return new WaitForSeconds(shootRate);
    }

    IEnumerator LaserShoot()
    {
        yield return new WaitForSeconds(shootRate);
    }

    IEnumerator PistolShoot()
    {
        yield return new WaitForSeconds(shootRate);
    }

    IEnumerator BazookaShoot()
    {
        yield return new WaitForSeconds(shootRate);
    }

    IEnumerator SMGShoot()
    {
        yield return new WaitForSeconds(shootRate);
    }

    IEnumerator ARShoot()
    {
        yield return new WaitForSeconds(shootRate);
    }
}
