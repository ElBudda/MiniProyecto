using UnityEngine;
using static InventoryManager;

public class Item : MonoBehaviour
{
    [Header("Item Settings")]
    public string itemName = "Unknown";
    public Sprite icon;
    public ItemType itemType = ItemType.Other;

    [Header("Food Settings (if applicable)")]
    public int foodValue = 0;

    [Header("Collectability")]
    public bool isCollectible = true;

    public void OnCollect()
    {
        if (!isCollectible) return;

        // ⭐ If Food, immediately add food points
        if (itemType == ItemType.Food)
        {
            PlayerFoodSystem.Instance.AddFoodPoints(foodValue);
            Debug.Log($" Ate {itemName} (+{foodValue} points) immediately on pickup!");
        }
        else if (itemType == ItemType.Trash)
        {
            InventoryManager.Instance.trashPoints += 1;
            Debug.Log($" Collected Trash! Total Trash Points: {InventoryManager.Instance.trashPoints}");
        }

        // Add to Inventory (optional - can keep or skip)
        InventoryManager.Instance.AddItem(
            itemName,
            itemType,
            foodValue,
            icon
        );

        Debug.Log($" Collected: {itemName} ({itemType})");

        gameObject.SetActive(false);
    }

}
