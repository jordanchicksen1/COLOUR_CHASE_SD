using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public bool isShooting;
    public List<bool> Guns;
    [SerializeField]
    private float shootRate;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private List <Transform> bulletPoints;
    [SerializeField]
    private int bulletSpeed;
    public bool isRunning;
    private bool isReLoading;
    [SerializeField]
    private float ReloadTime;
    [SerializeField]
    private float bulletLife;
    [SerializeField]
    private string BulletTag;
    public AudioSource shootSFX;
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
            shootSFX.Play();
            for(int i = 0; i < bulletPoints.Count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPoints[i].position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletPoints[i].right * bulletSpeed;
                Destroy(bullet, bulletLife);
            }


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
        if (!isReLoading)
        {
            shootSFX.Play();
            for (int i = 0; i < bulletPoints.Count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPoints[i].position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletPoints[i].right * bulletSpeed;
                bullet.tag = BulletTag;
                Destroy(bullet, bulletLife);
            }


            StartCoroutine(ReloadChecker());
            yield return new WaitForSeconds(shootRate);

            if (isShooting)
            {
                StartCoroutine(SniperShoot());
            }
        }
    }

    IEnumerator LaserShoot()
    {
        if (!isReLoading)
        {
            shootSFX.Play();
            for (int i = 0; i < bulletPoints.Count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPoints[i].position, Quaternion.identity);
                bullet.tag = BulletTag;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletPoints[i].right * bulletSpeed;
                Destroy(bullet, bulletLife);
            }


            StartCoroutine(ReloadChecker());
            yield return new WaitForSeconds(shootRate);

            if (isShooting)
            {
                StartCoroutine(SniperShoot());
            }
        }
    }

    IEnumerator PistolShoot()
    {
        if (!isReLoading)
        {
            shootSFX.Play();
            for (int i = 0; i < bulletPoints.Count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPoints[i].position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletPoints[i].right * bulletSpeed;
                bullet.tag = BulletTag;
                Destroy(bullet, bulletLife);
            }


            StartCoroutine(ReloadChecker());
            yield return new WaitForSeconds(shootRate);

            if (isShooting)
            {
                StartCoroutine(SniperShoot());
            }
        }
    }

    IEnumerator BazookaShoot()
    {
        if (!isReLoading)
        {
            shootSFX.Play();
            for (int i = 0; i < bulletPoints.Count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPoints[i].position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletPoints[i].right * bulletSpeed;
                bullet.tag = BulletTag;
            }


            StartCoroutine(ReloadChecker());
            yield return new WaitForSeconds(shootRate);

            if (isShooting)
            {
                StartCoroutine(SniperShoot());
            }
        }
    }

    IEnumerator SMGShoot()
    {
        if (!isReLoading)
        {
            shootSFX.Play();
            for (int i = 0; i < bulletPoints.Count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPoints[i].position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletPoints[i].right * bulletSpeed;
                bullet.tag = BulletTag;
                Destroy(bullet, bulletLife);
                shootSFX.Stop();
            }


            StartCoroutine(ReloadChecker());
            yield return new WaitForSeconds(shootRate);

            if (isShooting)
            {
                StartCoroutine(SniperShoot());
            }
        }
    }

    IEnumerator ARShoot()
    {
        if (!isReLoading)
        {
            shootSFX.Play();
            for (int i = 0; i < bulletPoints.Count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPoints[i].position, Quaternion.identity);
                bullet.tag = BulletTag;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletPoints[i].right * bulletSpeed;
                Destroy(bullet, bulletLife);
            }


            StartCoroutine(ReloadChecker());
            yield return new WaitForSeconds(shootRate);

            if (isShooting)
            {
                StartCoroutine(SniperShoot());
            }
        }
    }
}
