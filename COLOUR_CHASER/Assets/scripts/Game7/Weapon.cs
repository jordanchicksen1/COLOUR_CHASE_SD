using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    BubbleBlaster,
    LaserGun,
    PumpShotgun,
    Pistol,
    HandCannon
}

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public WeaponType weaponType = WeaponType.None;
    public Transform firePoint; 
    public GameObject projectilePrefab;
    public float fireRate = 0.5f;
    public float recoil = 1f;

    [Header("VFX")]
    [SerializeField] private ParticleSystem muzzleFlash;

    private float nextFireTime;
    private Vector3 firePointOriginalLocalPos;

    void Awake()
    {
        if (firePoint != null)
            firePointOriginalLocalPos = firePoint.localPosition;
    }

    public void Shoot()
    {
        if (Time.time < nextFireTime) return;

        switch (weaponType)
        {
            case WeaponType.BubbleBlaster:
                ShootBubble();
                break;
            case WeaponType.LaserGun:
                ShootLaser();
                break;
            case WeaponType.PumpShotgun:
                ShootShotgun();
                break;
            case WeaponType.Pistol:
                ShootPistol();
                break;
            case WeaponType.HandCannon:
                ShootHandCannon();
                break;
        }

        PlayMuzzleFlash();

        nextFireTime = Time.time + fireRate;
    }

    void ShootBubble()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Bubble Blaster Fired!");
    }

    void ShootLaser()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Laser Fired!");
    }

    void ShootShotgun()
    {
        for (int i = -2; i <= 2; i++)
        {
            Quaternion spread = firePoint.rotation * Quaternion.Euler(0, 0, i * 5f);
            Instantiate(projectilePrefab, firePoint.position, spread);
        }
        Debug.Log("Shotgun blast!");
    }

    void ShootPistol()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Pistol Fired!");
    }

    void ShootHandCannon()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Hand Cannon Blast!");
    }

    public void FlipFirePoint(bool facingRight)
    {
        Vector3 localPos = firePointOriginalLocalPos; 
        float extraOffset = 0.5f; 
        localPos.x = (facingRight ? 1 : 1) * Mathf.Abs(localPos.x + extraOffset);
        firePoint.localPosition = localPos;

        firePoint.localRotation = Quaternion.Euler(0, 0, facingRight ? 180f : 0f);

    }

    void PlayMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.transform.position = firePoint.position;
            muzzleFlash.transform.rotation = firePoint.rotation;
            muzzleFlash.Play();
        }
    }
}