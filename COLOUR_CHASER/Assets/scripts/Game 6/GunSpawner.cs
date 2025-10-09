using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> GunPoints;
    [SerializeField]
    private List<GameObject> Guns;


    private void Start()
    {
        SpawnGun();
        StartCoroutine(CheckForGuns());

    }

    private void Update()
    {
        
    }
    void SpawnGun()
    {
        for (int i = 0; i < GunPoints.Count; i++)
        {
            if (GunPoints[i].childCount == 0)
            {
                GameObject Gun = Instantiate(Guns[Random.Range(0, GunPoints.Count)], GunPoints[i].position, Quaternion.identity);
                Gun.transform.parent = GunPoints[i].transform;
            }
        }
    }

    IEnumerator CheckForGuns()
    {
        yield return new WaitForSeconds(30f);
        SpawnGun();
        StartCoroutine(CheckForGuns());
    }


}
