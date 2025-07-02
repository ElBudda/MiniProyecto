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

        InventoryManager.Instance.AddItem(
            itemName,
            itemType,
            foodValue,
            icon
        );

        Debug.Log($"Collected: {itemName} ({itemType})");

        gameObject.SetActive(false);
    }
}


