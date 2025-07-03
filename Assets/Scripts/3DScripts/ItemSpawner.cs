using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] possiblePrefabs;
    public float spawnRadius = 1.0f;
    public float spawnHeight = 10f;
    public float fixedHeightAboveGround = 0.2f;
    public LayerMask groundLayer;

    private GameObject currentItem;

    public GameObject foodPrefab;  // Assign in Inspector

    public void ClearItem()
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
            currentItem = null;
        }
    }

    public void ForceSpawnFood()
    {
        if (foodPrefab == null)
        {
            Debug.LogError("No FoodPrefab assigned to ItemSpawner!");
            return;
        }

        TrySpawnItem(foodPrefab);
    }

    public void SpawnRandom()
    {
        if (possiblePrefabs.Length == 0)
        {
            Debug.LogError("No possiblePrefabs assigned!");
            return;
        }

        GameObject chosen = possiblePrefabs[Random.Range(0, possiblePrefabs.Length)];
        TrySpawnItem(chosen);
    }

    private void TrySpawnItem(GameObject prefab)
    {
        Vector3 spawnPos;
        if (GetUniformGroundPosition(out spawnPos))
        {
            currentItem = Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("ItemSpawner: Couldn't find valid ground position to spawn.");
        }
    }

    private bool GetUniformGroundPosition(out Vector3 groundPoint)
    {
        // Pick random point within spawn radius around spawner base
        Vector2 offset2D = Random.insideUnitCircle * spawnRadius;
        Vector3 testPoint = transform.position + new Vector3(offset2D.x, 0, offset2D.y);

        // Raycast DOWN from above to find the actual ground
        Ray ray = new Ray(testPoint + Vector3.up * spawnHeight, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, spawnHeight * 2f, groundLayer))
        {
            // Always add exact, consistent offset above ground
            groundPoint = hit.point + Vector3.up * fixedHeightAboveGround;
            return true;
        }

        groundPoint = Vector3.zero;
        return false;
    }
}
