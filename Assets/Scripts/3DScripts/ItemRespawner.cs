using UnityEngine;
using System.Collections.Generic;

public class ItemRespawner : MonoBehaviour
{
    public static ItemRespawner Instance;

    [Header("All Item Spawners")]
    public ItemSpawner[] allSpawners;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        RespawnAllItems();
    }
    public void RespawnAllItems()
    {
        Debug.Log("🌅 New Day: Respawning items...");

        // 1️⃣ Clear everything
        foreach (var spawner in allSpawners)
        {
            spawner.ClearItem();
        }

        // 2️⃣ Determine how much Food is needed
        int foodNeeded = PlayerFoodSystem.Instance.requiredFoodPoints;
        Debug.Log($"✅ Need at least {foodNeeded} food items this day!");

        // 3️⃣ Shuffle all spawners
        List<ItemSpawner> shuffledSpawners = new List<ItemSpawner>(allSpawners);
        ShuffleList(shuffledSpawners);

        // 4️⃣ Force Food spawns
        int foodSpawned = 0;
        foreach (var spawner in shuffledSpawners)
        {
            if (foodSpawned < foodNeeded)
            {
                spawner.ForceSpawnFood();
                foodSpawned++;
            }
        }

        // 5️⃣ Fill the rest randomly
        foreach (var spawner in shuffledSpawners)
        {
            if (spawner.transform.childCount == 0 && spawner != null)
            {
                spawner.SpawnRandom();
            }
        }

        Debug.Log($"✅ Food Spawned: {foodSpawned}, Total Spawners: {allSpawners.Length}");
    }

    private void ShuffleList(List<ItemSpawner> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            ItemSpawner temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}


