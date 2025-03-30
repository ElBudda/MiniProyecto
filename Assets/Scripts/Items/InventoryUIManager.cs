using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public Inventory inventory;
    public Transform inventoryPanel; // UI container for inventory items
    public GameObject inventorySlotPrefab; // Prefab for UI slots

    public void UpdateUI()
    {
        // Clear previous UI slots
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Add new UI slots
        foreach (var item in inventory.items)
        {
            ItemData itemData = inventory.GetItemData(item.Key);
            if (itemData == null) continue;

            GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);
            newSlot.GetComponent<Image>().sprite = itemData.icon;
            newSlot.GetComponentInChildren<Text>().text = item.Value.ToString(); // Show count
        }
    }
}
