using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemsToSpawn;  // Array of possible items
    public Transform[] spawnPoints;    // Array of spawn locations
    public float spawnInterval = 5f;   // Time between spawns

    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomItem = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];

            Instantiate(randomItem, randomSpawnPoint.position, Quaternion.identity);
        }
    }
}

