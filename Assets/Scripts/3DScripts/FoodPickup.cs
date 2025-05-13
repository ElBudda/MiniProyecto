using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public string foodName = "Bread Slice";
    public int foodValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddFood(foodName, foodValue);
            Destroy(gameObject);
        }
    }
}

