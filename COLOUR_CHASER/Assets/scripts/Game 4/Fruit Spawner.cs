using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SpawnPoints;
    [SerializeField]
    private List<GameObject> Fruit;
    [SerializeField]
    private float spawnRate;

    private void Start()
    {
        StartCoroutine(SpawnFruit());
    }
    IEnumerator SpawnFruit()
    {
        AssignFruit();
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnFruit());

    }

    void AssignFruit()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            Instantiate(Fruit[Random.Range(0, Fruit.Count)], SpawnPoints[i].position, Quaternion.identity);
        }
    }
}
