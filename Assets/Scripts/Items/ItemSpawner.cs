using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemsToSpawn;  // Array of possible items
    public Transform[] spawnPoints;    // Array of spawn locations
    public float spawnInterval = 5f;   // Time between spawn attempts
    private Dictionary<Transform, GameObject> spawnedItems = new Dictionary<Transform, GameObject>();

    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            foreach (Transform spawnPoint in spawnPoints)
            {
                // If there's no item at this spawn point, spawn a new one
                if (!spawnedItems.ContainsKey(spawnPoint) || spawnedItems[spawnPoint] == null)
                {
                    GameObject randomItem = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];
                    GameObject spawnedItem = Instantiate(randomItem, spawnPoint.position, Quaternion.identity);

                    // Track the spawned item
                    spawnedItems[spawnPoint] = spawnedItem;
                }
            }
        }
    }
}


