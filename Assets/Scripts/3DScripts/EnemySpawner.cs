using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("References")]
    public GameObject catPrefab;
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public int baseCats = 1;
    public int catsPerDay = 1;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnForStartOfDay(int day)
    {
        ClearExistingCats();
        SpawnForDay(day);
    }

    public void SpawnForDay(int day)
    {
        ClearExistingCats();

        int total = baseCats + (catsPerDay * (day - 1));
        Debug.Log($"[EnemySpawner] Spawning {total} cats for Day {day}");

        for (int i = 0; i < total; i++)
        {
            Vector3 spawnPoint = ChooseRandomPoint();
            Instantiate(catPrefab, spawnPoint, Quaternion.identity);
        }
    }

    Vector3 ChooseRandomPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("[EnemySpawner] No spawn points set!");
            return Vector3.zero;
        }

        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index].position;
    }

    void ClearExistingCats()
    {
        EnemyCat[] existingCats = FindObjectsOfType<EnemyCat>();
        foreach (var cat in existingCats)
        {
            Destroy(cat.gameObject);
        }
    }
}


