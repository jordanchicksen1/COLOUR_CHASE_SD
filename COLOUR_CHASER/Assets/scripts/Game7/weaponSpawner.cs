using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSpawner : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject[] weaponPrefabs; 
    public Transform[] spawnPoints;    
    public float respawnDelay = 20f;

    private GameObject[] currentWeapons;

    void Start()
    {
        currentWeapons = new GameObject[spawnPoints.Length];
        SpawnWeapons();
    }

    void SpawnWeapons()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (currentWeapons[i] == null && weaponPrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, weaponPrefabs.Length);
                GameObject weapon = Instantiate(
                    weaponPrefabs[randomIndex],
                    spawnPoints[i].position,
                    Quaternion.identity
                );

                currentWeapons[i] = weapon;

                Weapon weaponScript = weapon.GetComponent<Weapon>();
                if (weaponScript != null)
                    weaponScript.RegisterSpawner(this);
            }
        }
    }

    public void OnWeaponPickedUp(GameObject weapon)
    {
        for (int i = 0; i < currentWeapons.Length; i++)
        {
            if (currentWeapons[i] == weapon)
            {
                currentWeapons[i] = null;
                StartCoroutine(RespawnWeaponAfterDelay(i));
                break;
            }
        }
    }

    IEnumerator RespawnWeaponAfterDelay(int index)
    {
        yield return new WaitForSeconds(respawnDelay);

        if (weaponPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, weaponPrefabs.Length);
            GameObject newWeapon = Instantiate(
                weaponPrefabs[randomIndex],
                spawnPoints[index].position,
                Quaternion.identity
            );

            currentWeapons[index] = newWeapon;

            Weapon weaponScript = newWeapon.GetComponent<Weapon>();
            if (weaponScript != null)
                weaponScript.RegisterSpawner(this);
        }
    }
}
